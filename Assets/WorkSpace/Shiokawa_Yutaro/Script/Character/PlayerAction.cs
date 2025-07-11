using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction
{

    //public void AcceptInput()
    //{
    //    while (true)
    //    {
    //        // �ړ��̎�t
    //        if (AcceptMove()) break;
    //        // �U���̎�t
    //        if (await AcceptAttack()) break;
    //        //�����]�����͂̎�t����
    //        await AcceptDirChange();
    //    }
    //}

    /// <summary>
    /// �ړ��̎�t�A��������
    /// </summary>
    /// <returns>�ړ�������True</returns>
    public bool AcceptMove()
    {
        // 8�����̓��͂��󂯕t����
        Vector3 inputDir = AcceptDirInput().normalized;
        if (inputDir.magnitude <= 0.0f) return false;
        // �ړ��ۂ̔���
        CharacterBase player = GetPlayer();

        // �󂯕t�������͂ɉ����Ĉړ�
        player.SetPosition();
        return true;
    }

    private Vector3 AcceptDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {

            }
        }
        return Vector3.zero;
    }
    /// <summary>
    /// �ʏ�U�����͎�t�A����
    /// </summary>
    /// <returns></returns>
    private bool AcceptAttack()
    {
        if (!Input.GetMouseButton(0)) return false;

        ExecuteAction(GetPlayer(), NORMAL_ATTACK_ACTION_ID);
        return true;
    }

    private void AcceptDirChange()
    {
        if (!Input.GetKey(KeyCode.Space)) return;

        CharacterBase player = GetPlayer();
        //�����]�����͂̃g���K�[���󂯕t���A�אڃG�l�~�[�̕����������G�Ɍ���
        //�W�����v
    }

    private void ChangeDirToEnemy(CharacterBase character)
    {
        //�X�^�[�g�̌���
        int startIndex = (int)character.direction + 1;
        int basePosX = character.posX, basePosY = character.posY;
        //�W�����̗אڃ}�X�ő����A�G�l�~�[��T���A������ς��}�X�̐F��ς���
        Vector3.Dot()
    }
}
