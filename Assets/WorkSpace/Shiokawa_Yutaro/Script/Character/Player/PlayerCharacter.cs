using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    private PlayerAction _playerAction;

    public override void Setup()
    {
        _playerAction = GetComponent<PlayerAction>();
        speed = 2.5f;
        maxHP = 10;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        SetStatus();
    }
    private void Update()
    {
        //プレイヤーの操作の呼び出し
        _playerAction.AcceptInput();
    }

    /// <summary>
    /// プレイヤーかどうか
    /// </summary>
    public override bool IsPlayer()
    {
        return true;
    }
    /// <summary>
    /// 死亡時処理
    /// </summary>
    public override void Dead()
    {
        // プレイヤー死亡でダンジョン終了
       // _EndDungeon?.Invoke(eDungeonEndReason.Dead);
    }

    public override UniTask GoingAttack()
    {
        throw new System.NotImplementedException();
    }

    public override UniTask LongRangeAttack()
    {
        throw new System.NotImplementedException();
    }

    public override UniTask TakeDistance()
    {
        throw new System.NotImplementedException();
    }

    public override UniTask CounterAttack()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            if (_playerAction.isDashing)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
