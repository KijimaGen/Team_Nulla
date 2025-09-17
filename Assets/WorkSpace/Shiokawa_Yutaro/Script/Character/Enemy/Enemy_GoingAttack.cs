using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GoingAttack : EnemyCharacter
{
    public override void Setup()
    {
        attackStrategies = new Dictionary<AttackType, AttackStrategy>
        {
            { AttackType.Going, new GoingAttack() },
            { AttackType.TakeDistance, new TakeDistance() }
        };

        attackArea = 0.5f;
        speed = 1.5f;
        maxHP = 15;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        base.Setup();
    }

    public override async UniTask GoingAttack()
    {
        animation.Play("攻撃1");

        Attack(player.transform.position);

        // アニメーションの終了を待つ（基底のクラスの関数）
        // await WaitUntilAnimationStateExits(attackName); // ←"Attack"はアニメーターのステート名

    }

    protected override void HitEffect(Vector3 hitPos)
    {
        animation.Play("ダメージを受ける");
        base.HitEffect(hitPos);
    }
}
