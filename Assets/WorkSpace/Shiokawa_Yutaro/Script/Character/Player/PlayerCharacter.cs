using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    private PlayerAction _playerAction;

    public override void Setup()
    {
        _playerAction = GetComponent<PlayerAction>();
        speed = 3;
        maxHP = 10;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        SetStatus();
    }
    private void Update()
    {
        //�v���C���[�̑���̌Ăяo��
        _playerAction.AcceptInput();
    }

    /// <summary>
    /// �v���C���[���ǂ���
    /// </summary>
    public override bool IsPlayer()
    {
        return true;
    }
    /// <summary>
    /// ���S������
    /// </summary>
    public override void Dead()
    {
        // �v���C���[���S�Ń_���W�����I��
       // _EndDungeon?.Invoke(eDungeonEndReason.Dead);
    }
}
