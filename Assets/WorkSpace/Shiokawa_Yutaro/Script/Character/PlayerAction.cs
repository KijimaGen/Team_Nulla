using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using static CharacterManager;
using static CharacterUtility;

public class PlayerAction : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AcceptInput()
    {
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
        if (AcceptJump()) return false;
        // 8方向の入力を受け付ける
        Vector3 inputDir = AcceptDirInput().normalized;
        if (inputDir.magnitude <= 0.0f) return false;

        //視点入力の受付処理
        AcceptDirChange(inputDir);

        // 移動可否の判定
        PlayerCharacter player = GetComponent<PlayerCharacter>();
        transform.position += inputDir * player.speed * Time.deltaTime;

        return true;
    }

    private bool AcceptJump()
    {
        if (!Input.GetKey(KeyCode.Space)) return false;

        rb.DOMove(Vector3.up * 3, 0.5f);

        return true;
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

        float rotationSpeed = 720f;
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


}
