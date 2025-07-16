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
        // �ړ��̎�t
        if (AcceptMove()) return;
        // �U���̎�t
        if (AcceptAttack()) return;
        
    }

    /// <summary>
    /// �ړ��̎�t�A��������
    /// </summary>
    /// <returns>�ړ�������True</returns>
    public bool AcceptMove()
    {
        if (AcceptJump()) return false;
        // 8�����̓��͂��󂯕t����
        Vector3 inputDir = AcceptDirInput().normalized;
        if (inputDir.magnitude <= 0.0f) return false;

        //���_���͂̎�t����
        AcceptDirChange(inputDir);

        // �ړ��ۂ̔���
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
        float moveX = Input.GetAxisRaw("Horizontal"); // ����
        float moveZ = Input.GetAxisRaw("Vertical");   // ����

        Vector3 input = new Vector3(moveX, 0, moveZ);
        input = Vector3.ClampMagnitude(input, 1f); // �΂߈ړ���␳

        // �J�����̌����ɍ��킹�ē��͂���]������
        Transform cam = Camera.main.transform;

        // �J������Y�������̉�]�������o��
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        // �J������̓��͕�����Ԃ�
        Vector3 moveDir = camForward * input.z + camRight * input.x;

        return moveDir;
    }

    /// <summary>
    /// �ʏ�U�����͎�t�A����
    /// </summary>
    /// <returns></returns>
    private bool AcceptAttack()
    {
        if (!Input.GetMouseButton(0)) return false;

        //ExecuteAction(GetPlayer(), NORMAL_ATTACK_ACTION_ID);
        //�������Ă镐����Q�Ƃ�����


        return true;
    }

    /// <summary>
    /// ���_����
    /// </summary>
    private void AcceptDirChange(Vector3 dir)
    {
        if (dir == Vector3.zero) return;

        float rotationSpeed = 720f;
        // ���͕���������Quaternion���쐬�iY���̂݁j
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        Vector3 euler = targetRotation.eulerAngles;
        targetRotation = Quaternion.Euler(0, euler.y, 0);

        // ���݂̉�]����AtargetRotation�ցArotationSpeed�x/�b�ŉ�]
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }


}
