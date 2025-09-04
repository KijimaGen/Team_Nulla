using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    Rigidbody rb;

    private float shiftPressTime = 0f;
    public bool isDashing = false;
    private float dashThreshold = 0.1f; // 0.25秒以上でダッシュ扱い
    private bool isJumping = false;
    public bool isAvoiding = false;
    public bool isCounter = false;

    private float AvoidingCoolInterval = 1f;

    private float pickupRadius = 1f;

    //アニメーション
    private Animation animation;

    private bool inputShiftButton;

    private PlayerCharacter player;

    [SerializeField] ParticleSystem attackEfect;
    [SerializeField] ParticleSystem counterEfect;

    Vector3 switchLvale;

    private void Start()
    {
        player = GetComponent<PlayerCharacter>();

        rb = GetComponent<Rigidbody>();
        animation = GetComponent<Animation>();

        foreach (AnimationState state in animation)
        {
            Debug.Log("登録済みアニメーション: " + state.name);
        }
    }

    public void AcceptInput()
    {

        if (isJumping && IsGrounded())
        {
            isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupItem();
        }
        //攻撃の受付
        if (AcceptAttack()) return;
        //ジャンプの受付
        if (AcceptJump()) return;
        //移動の受付
        if (AcceptMove()) return;
        

    }

    /// <summary>
    /// 移動の受付、内部処理
    /// </summary>
    /// <returns>移動したらTrue</returns>
    public bool AcceptMove()
    {
        if (isJumping || player.isAttacking) return false;

        Vector3 inputDir = AcceptDirInput().normalized;
        if (inputDir.magnitude <= 0.0f)
        {
            player.SetSpeed(player.walkSpeed);
            isDashing = false;
            isAvoiding = false;
            isCounter = false;
            return false;
        }

        AcceptDirChange(inputDir);

        if (isDashing)
        {
            player.SetSpeed(player.runSpeed);
            TriggerDash();
            //Debug.Log("ダッシュの開幕時間 : " + shiftPressTime);

            //ダッシュし続けていたら
            shiftPressTime += Time.deltaTime;
            if (shiftPressTime >= dashThreshold)
            {
                isAvoiding = false;
            }
            else if (shiftPressTime < dashThreshold)
            {
                TriggerDodge(player);
            }

        }
        else
        {
            shiftPressTime = 0f;
        }


        if (Input.GetKey(KeyCode.LeftShift) && !inputShiftButton)
        {
            inputShiftButton = true;
            isDashing = true;

            if (shiftPressTime >= AvoidingCoolInterval) { shiftPressTime = 0f; }

        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            inputShiftButton = false;
        }

        Vector3 velocity = rb.velocity;
        velocity.x = inputDir.x * player.speed;
        velocity.z = inputDir.z * player.speed;
        rb.velocity = velocity;

        return true;
    }

    private bool AcceptJump()
    {
        if (!IsGrounded() || isAvoiding) return false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 水平方向の移動を止める
            rb.velocity = new Vector3(0f, 3f, 0f);
            isJumping = true;
            return true;
        }


        return false;
    }


    private bool IsGrounded()
    {
        float rayLength = 2f;
        Vector3 origin = transform.position;
        return Physics.Raycast(origin, Vector3.down, rayLength);
    }

    /// <summary>
    /// 回避処理
    /// </summary>
    /// <param name="player"></param>
    private void TriggerDodge(PlayerCharacter player)
    {
        isAvoiding = true;
        Debug.Log("回避発動！");
        // プレイヤーの前方向に瞬間的に動かす（例: 回避ロール）
        Vector3 dodgeDir = AcceptDirInput().normalized;
        if (dodgeDir == Vector3.zero)
        {
            dodgeDir = transform.forward; // 入力がなければ前に回避
        }

        //※ジャスト回避システム

        rb.AddForce(dodgeDir * player.runSpeed * 200, ForceMode.Impulse);
    }

    /// <summary>
    /// ダッシュ中の銃弾弾き処理
    /// </summary>
    private void TriggerDash()
    {
        //回避中なら外れる
        if (isAvoiding) return;
        if (isCounter) return;
        //弾を弾く(ずっと繰り返す)
        //ParticleSystem effect = Instantiate(counterEfect, transform.position + Vector3.up * 0.3f, Quaternion.LookRotation(transform.forward));
        //effect.transform.SetParent(transform);
        //アニメーション(一度だけ)

        animation.Play("ダッシュ");
        //Debug.Log("プレイやーがダッシュ発動");
        isCounter = true;
    }

    /// <summary>
    /// 方向入力と自機回転処理
    /// </summary>
    /// <returns></returns>
    public Vector3 AcceptDirInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // ←→
        float moveZ = Input.GetAxisRaw("Vertical");   // ↑↓

        Vector3 swicthMoveValue= switchLvale;
        Debug.Log("LStickValue" + switchLvale);
        Vector3 input = new Vector3(swicthMoveValue.x, 0, swicthMoveValue.z);
        input = Vector3.ClampMagnitude(input, 1f); // 斜め移動を補正

        // カメラの向きに合わせて入力を回転させる
        Transform cam = Camera.main.transform;

        // カメラのY軸方向の回転だけ取り出す
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        // カメラ基準の入力方向を返す
        Vector3 moveDir = camForward * input.z + camRight * input.x;

        return moveDir;
    }

    public void SwitchMove(InputAction.CallbackContext context)
    {
        switchLvale = context.ReadValue<Vector3>();
    }

    /// <summary>
    /// 通常攻撃入力受付、処理
    /// </summary>
    /// <returns></returns>
    bool inputAttack;
    private bool AcceptAttack()
    {
        if (Input.GetMouseButton(0) && !inputAttack && !isJumping)
        {
            inputAttack = true;
            if (!player.isAttacking)
            {
                TryAttackNearestEnemy().Forget();
                return true;
            }
        }
        else if(!Input.GetMouseButton(0))
        {
            inputAttack = false;
            return false;
        }
        
        Debug.Log("プレイヤーの攻撃");
        //近くの敵に向いて攻撃する補正をつける
        

        

        //ロックオンでターゲット取ってるか

        //ExecuteAction(GetPlayer(), NORMAL_ATTACK_ACTION_ID);
        //今持ってる武器を参照したい


        return true;
    }

    /// <summary>
    /// 視点操作
    /// </summary>
    private void AcceptDirChange(Vector3 dir)
    {
        if (dir == Vector3.zero) return;

        float rotationSpeed = 2000f;
        // 入力方向を向くQuaternionを作成（Y軸のみ）
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        Vector3 euler = targetRotation.eulerAngles;
        targetRotation = Quaternion.Euler(0, euler.y, 0);

        // 現在の回転から、targetRotationへ、rotationSpeed度/秒で回転
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void TryPickupItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRadius);

        PickItem closestItem = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            PickItem item = hit.GetComponent<PickItem>();
            if (item == null) continue;

            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }

        if (closestItem != null)
        {
            Pickup(closestItem);
        }
    }

    private void Pickup(PickItem item)
    {
        Debug.Log($"拾ったアイテム: {item.itemName}");

        // 例: 所持品に追加する、UI更新など
        // inventory.Add(item);

        // アイテムを消す
        Destroy(item.gameObject);
    }

    private float attackRange;       // 攻撃できる範囲
    private int enemyLayer;

    private async UniTaskVoid TryAttackNearestEnemy()
    {
        
        if (player.isAttacking) return;
        ParticleSystem effect = Instantiate(attackEfect,transform.position + Vector3.up * 0.3f, Quaternion.LookRotation(transform.forward));
        effect.transform.SetParent(transform);
        Destroy(effect.gameObject,2);
        enemyLayer = LayerMask.GetMask("Enemy");
        attackRange = 2;

        // 攻撃範囲内の敵を探す
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        if (hits.Length == 0) return;

        // 一番近い敵を選択
        var nearestEnemy = hits
            .Select(h => h.GetComponent<EnemyCharacter>())
            .Where(e => e != null && !e.isDead)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();

        //if (nearestEnemy == null) return;

        player.Attack("a", nearestEnemy.transform.position);

        // 攻撃アニメーション実行
        await player.GoingAttack();

        // 敵との距離を測って「目の前の位置」を計算
        float stopDistance = 0.5f; // 武器の射程（敵の手前で止まる距離）
        Vector3 dir = (nearestEnemy.transform.position - transform.position).normalized;
        Vector3 stopPos = nearestEnemy.transform.position - dir * stopDistance;

        // 近距離武器なら、そこまでダッシュ移動
        rb.DOMove(stopPos, 0.1f).OnComplete(() =>
        {
            player.isAttacking = false;
        });        
    }

    private void OnDrawGizmosSelected()
    {
        // 攻撃範囲を可視化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);


    }
}
