using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using static CharacterManager;
using static CharacterUtility;
using static UnityEditor.Progress;

public class PlayerAction : MonoBehaviour
{
    Rigidbody rb;
    bool isJump = false;

    private float shiftPressTime = 0f;
    public bool isDashing = false;
    private float dashThreshold = 0.25f; // 0.25秒以上でダッシュ扱い
    private bool isJumping = false;
    public bool isAvoiding = false;

    private float pickupRadius = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        if (AcceptJump()) return;
        // 移動の受付
        if (AcceptMove()) return;
        // 攻撃の受付
        if (AcceptAttack()) return;
        
    }

    /// <summary>
    /// 移動の受付、内部処理
    /// </summary>
    /// <returns>移動したらTrue</returns>
    public bool AcceptMove()
    {
        if (!IsGrounded() || isJumping) return false;

        Vector3 inputDir = AcceptDirInput().normalized;
        if (inputDir.magnitude <= 0.0f) return false;

        AcceptDirChange(inputDir);

        PlayerCharacter player = GetComponent<PlayerCharacter>();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftPressTime += Time.deltaTime;

            if (shiftPressTime >= dashThreshold)
            {
                isAvoiding = false;
                isDashing = true;
                player.SetSpeed(5);
            }
            else if (shiftPressTime < dashThreshold)
            {
                TriggerDodge(player);
            }
        }
        else
        {
            shiftPressTime = 0f;
            isDashing = false;
            player.SetSpeed(3);
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
        float rayLength = 0.2f;
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, rayLength);
    }


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

        rb.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
    }


    private Vector3 AcceptDirInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // ←→
        float moveZ = Input.GetAxisRaw("Vertical");   // ↑↓

        Vector3 input = new Vector3(moveX, 0, moveZ);
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
    private bool AcceptAttack()
    {
        if (!Input.GetMouseButton(0)) return false;

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


}
