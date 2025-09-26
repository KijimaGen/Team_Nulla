using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    Rigidbody rb;

    private float shiftPressTime = 0f;
    public bool isDashing = false;
    private float dashThreshold = 0.1f; // 0.25秒以上でダッシュ扱い
    private bool isJumping = false;
    public bool isAvoiding = false;
    public bool isJustAvoiding = false;
    public bool isCounter = false;

    private float AvoidingCoolInterval = 1f;

    private float pickupRadius = 1f;

    //アニメーション
    private Animator animator;

    private bool inputShiftButton;


    [SerializeField] private Image specialGauge;

    private PlayerCharacter player;
    float currentRawAttack;

    [SerializeField] ParticleSystem attackEfect;
    [SerializeField] ParticleSystem smokeEffect;
    [SerializeField] ParticleSystem counterEfect;

    [SerializeField] ParticleSystem jumpEfect;

    [SerializeField] ParticleSystem specialEffect;

    [SerializeField] Image switchZRButtonImage;
    [SerializeField] Image switchYButtonImage;
    [SerializeField] Image switchXButtonImage;

    [SerializeField] Transform[] special;


    Vector2 switchLStickValue;
    bool switchZRButton;
    bool switchBButton;
    bool switchYButton;
    bool switchXButton;

    [SerializeField] private GameObject WeaponModel;
    private void Start()
    {
        player = this.GetComponent<PlayerCharacter>();
        currentRawAttack = player.rawAttack;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        specialGauge.fillAmount = 1;
    }

    public void AcceptInput()
    {
        
        if (player == null) return;
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
    float justAvoidingTime = 0.05f;
    /// <summary>
    /// 移動の受付、内部処理
    /// </summary>
    /// <returns>移動したらTrue</returns>
    public bool AcceptMove()
    {
        if (isJumping || player.isAttacking) return false;

        Vector3 inputDir = AcceptDirInput().normalized;
       // Debug.Log(inputDir);
        if (inputDir.magnitude <= 0.0f)
        {
            player.SetSpeed(player.walkSpeed);
            isDashing = false;
            isAvoiding = false;
            isJustAvoiding = false;
            isCounter = false;
            animator.SetBool("Walk", false);
            animator.SetBool("Dash", false);

            // animation.Play("待機");

            return false;
        }

        AcceptDirChange(inputDir);

        if (isDashing)
        {
            Vector3 v = rb.velocity;
            v.y = 0;
            rb.velocity = v;
            animator.SetBool("Walk", false);
            animator.SetBool("Dash", true);
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
            if (shiftPressTime > justAvoidingTime)
            {
                isJustAvoiding = false;
            }

        }
        else
        {
            shiftPressTime = 0f;
            animator.SetBool("Walk", true);

        }


        if (switchZRButton && !inputShiftButton)
        {
            inputShiftButton = true;
            isDashing = true;

            if (shiftPressTime >= AvoidingCoolInterval) { shiftPressTime = 0f; }

        }
        else if (!switchZRButton)
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

        if (switchBButton)
        {
            ParticleSystem jumpEffect = Instantiate(jumpEfect, transform.position, Quaternion.identity);
            Destroy(jumpEffect.gameObject, 2);
            // 水平方向の移動を止める
            Jump();
            isJumping = true;

            AudioManager.instance.PlaySE(5);
            return true;
        }


        return false;
    }

    private float jumpHeight = 1f;   // ジャンプの高さ
    private float jumpDistance = 5f; // 飛びたい距離
    private float gravity = -9.81f;  // 重力（Unityのデフォルト）
    void Jump()
    {
        float jumpVelocity = Mathf.Sqrt(2 * -gravity * jumpHeight);
        rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
    }


    private bool IsGrounded()
    {
        float rayLength = 0.1f;
        Vector3 origin = transform.position;
        origin.y += 0.05f;
        Debug.DrawRay(origin, Vector3.down * rayLength);
        return Physics.Raycast(origin, Vector3.down, rayLength);
    }


    /// <summary>
    /// 回避処理
    /// </summary>
    /// <param name="player"></param>
    private void TriggerDodge(PlayerCharacter player)
    {
        isAvoiding = true;
        isJustAvoiding = true;


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

        animator.SetBool("Dash",true);

        //Debug.Log("プレイやーがダッシュ発動");
        isCounter = true;
    }

    /// <summary>
    /// 方向入力と自機回転処理
    /// </summary>
    /// <returns></returns>
    public Vector3 AcceptDirInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(switchLStickValue.x + moveX, 0, switchLStickValue.y + moveZ);
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

    /// <summary>
    /// 通常攻撃入力受付、処理
    /// </summary>
    /// <returns></returns>
    bool inputAttack;
    private bool AcceptAttack()
    {
        if (switchYButton && !inputAttack)
        {
            player.SetSpeed(player.walkSpeed);
            isDashing = false;
            inputAttack = true;
            
            if (isJumping)
            {
                TryAttackNearestEnemy().Forget();
                rb.velocity = Vector3.down;
                player.isAttacking = true;
                return true;
            }

            else if (canCombo)
            {
                TryAttackNearestEnemy().Forget();

                return true;
            }
            else
            {
                TryAttackNearestEnemy().Forget();
                return true;
            }
        }
        else if (switchXButton)
        {
            
            player.SetSpeed(player.walkSpeed);
            isDashing = false;
            inputAttack = true;
            
            player.isAttacking = true;

            return true;
        }

        else
        {
            return false;
        }

        //ExecuteAction(GetPlayer(), NORMAL_ATTACK_ACTION_ID);
        //今持ってる武器を参照したい

    }
    private async UniTaskVoid TryAttackNearestEnemy()
    {
        // 攻撃アニメーション実行
        animator.SetBool("Attack", true);
        player.isAttacking = true;
        //rb.velocity = Vector3.zero;

        enemyLayer = LayerMask.GetMask("Enemy");
        attackRange = 2;

        // 攻撃範囲内の敵を探す
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        if (hits.Length == 0) return;

        // 一番近い敵を選択
        var nearestEnemy = hits
            .Select(e => e.GetComponent<EnemyCharacter>())
            .Where(e => e != null && !e.isDead)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();
        if (nearestEnemy == null) return;

        player.Attack(nearestEnemy.transform.position);



        float stopDistance = 0.5f; //敵の手前で止まる距離
        Vector3 dir = (nearestEnemy.transform.position - transform.position).normalized;
        Vector3 stopPos = nearestEnemy.transform.position - dir * stopDistance;
        // 近距離武器なら、そこまでダッシュ移動
        rb.DOMove(stopPos, 0.1f);
    }

    private bool canCombo = false;
    public void OnComboOpen()
    {
        foreach (var t in special)
        {
            t.gameObject.SetActive(true);
        }
        
        GetComponent<CapsuleCollider>().enabled = true;

        animator.SetBool("Dash", false);
        animator.SetBool("Attack", false);
        canCombo = true;
        inputAttack = false;
        Destroy(currentHitbox);
    }
    public void OnComboClose()
    {
        animator.SetBool("Dash", false);
        animator.SetBool("Attack", false);
        player.isAttacking = false;
        canCombo = false;
        inputAttack = false;

        
    }

    public void SpecialAttackEffect()
    {
        Instantiate(specialEffect, transform.position + transform.up * 0.5f, Quaternion.identity);
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

    


    public void SwitchB(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switchBButton = true;
            Debug.Log(context.performed);
        }
        else
        {
            switchBButton = false;
        }
    }
    public void SwitchY(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switchYButton = true;
            switchYButtonImage.color = new Color32(100, 100, 100,255);
        }
        else
        {
            switchYButton = false;
            switchYButtonImage.color = new Color32(255, 255, 255, 255);
        }
    }
    public void SwitchX(InputAction.CallbackContext context)
    {
        if (context.performed && specialGauge.fillAmount == 1)
        {
            specialGauge.fillAmount = 0;
            switchXButton = true;
            switchXButtonImage.color = new Color32(100, 100, 100, 255);
            animator.SetTrigger("Special");
        }
        else
        {
            switchXButton = false;
            switchXButtonImage.color = new Color32(255, 255, 255, 255);
        }
    }
    public void SwitchZR(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switchZRButton = true;
            switchZRButtonImage.color = new Color32(100, 100, 100, 255);
        }
        else
        {
            switchZRButton = false;
            switchZRButtonImage.color = new Color32(255, 255, 255, 255);
        }
    }
    public void SwitchLStickMove(InputAction.CallbackContext context)
    {
        switchLStickValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 歩くときの音の再生
    /// </summary>
    public void PlayWalkSound() {
        AudioManager.instance.PlaySE(6);
    }

    /// <summary>
    /// 走る時の音の再生
    /// </summary>
    public void PlayDashSound() {
        AudioManager.instance.PlaySE(7);
    }

    public float AddHitPoint(float value)
    {
        return specialGauge.fillAmount += value;
    }

    public void Attack1()
    {
        player.SetRawAttack(currentRawAttack);
        ParticleSystem effect = Instantiate(attackEfect,transform.position + Vector3.up * 0.4f,Quaternion.identity);
        effect.transform.SetParent(transform);
        effect.transform.localRotation = Quaternion.Euler(0, 0, 30);
        //効果音を鳴らす
        AudioManager.instance.PlaySE(4);
        Destroy(effect.gameObject, 2);

        CreateHitBox(new Vector3(1f,0.7f,0.8f));
    }
    public void Attack2()
    {
        player.SetRawAttack(currentRawAttack * 1.2f);
        ParticleSystem effect = Instantiate(attackEfect, transform.position + Vector3.up * 0.4f, Quaternion.identity);
        effect.transform.SetParent(transform);
        effect.transform.localRotation = Quaternion.Euler(0, 0, -110);
        //効果音を鳴らす
        AudioManager.instance.PlaySE(4);
        Destroy(effect.gameObject, 2);

        CreateHitBox(new Vector3(0.5f, 1f, 0.8f));
    }
    public void Attack3()
    {
        player.SetRawAttack(currentRawAttack * 1.8f);
        ParticleSystem effect = Instantiate(attackEfect, transform.position + Vector3.up * 0.4f + transform.forward * 0.5f, Quaternion.identity);
        effect.transform.SetParent(transform);
        effect.transform.localRotation = Quaternion.Euler(0, 0, 60);
        AudioManager.instance.PlaySE(4);
        Destroy(effect.gameObject, 2);

        CreateHitBox(new Vector3(1.4f, 1f, 1.4f));
    }
    public void Attack3Motion()
    {
        rb.velocity = Vector3.up * 5;
    }
    public void Attack3End_Effect()
    {
        ParticleSystem effect = Instantiate(smokeEffect, transform.position + transform.forward * 0.5f + Vector3.up * 0.4f, Quaternion.identity);
        effect.transform.SetParent(transform);
        effect.transform.localScale = Vector3.one;
        Destroy(effect.gameObject, 2);
    }

    public void Attack_Dash1()
    {
        player.SetRawAttack(currentRawAttack * 1.2f);
        rb.velocity = transform.forward * 3;
        CreateHitBox(new Vector3(0.5f, 0.5f, 1f));
    }
    public void Attack_Dash2()
    {
        player.SetRawAttack(currentRawAttack * 1.4f);
        rb.velocity = transform.forward * 3;
        CreateHitBox(new Vector3(0.8f, 0.6f, 0.8f));
    }
    public void Attack_Special()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Walk", false);

        foreach (Transform t in special)
        {
            t.gameObject.SetActive(false);
        }

        ParticleSystem effect = Instantiate(specialEffect, transform.position + transform.forward * 0.5f + Vector3.up * 0.4f, Quaternion.identity);
        Destroy(effect.gameObject, 2);
        rb.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }


    private GameObject currentHitbox; // 前回生成したHitboxを保持する変数

    private void CreateHitBox(Vector3 boxSize)
    {
        // もし前回のヒットボックスが残っていたら削除
        if (currentHitbox != null)
        {
            Destroy(currentHitbox);
        }

        // 空のGameObjectを作成
        GameObject hitbox = new GameObject("AttackHitbox");
        hitbox.tag = "Weapon";
        hitbox.transform.parent = transform;

        // 位置・回転を設定
        hitbox.transform.position = transform.position + transform.forward * 0.5f;
        hitbox.transform.rotation = transform.rotation;
        // BoxCollider を付与
        BoxCollider collider = hitbox.AddComponent<BoxCollider>();
        collider.transform.localScale = boxSize * 3;
        Vector3 colPos = collider.transform.position;
        collider.transform.position = new Vector3(colPos.x, colPos.y + boxSize.y / 2f, colPos.z);
        collider.isTrigger = true;

        // デバッグ用に目に見える Cube を追加
        MeshFilter meshFilter = hitbox.AddComponent<MeshFilter>();
        meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");

        MeshRenderer renderer = hitbox.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.SetFloat("_Mode", 3);
        renderer.material.color = new Color(255f, 0f, 0f, 0.3f);
        renderer.enabled = false;
        currentHitbox = hitbox;
    }

}
