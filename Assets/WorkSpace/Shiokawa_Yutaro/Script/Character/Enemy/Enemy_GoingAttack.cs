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
        speed = Random.Range(1, 3);
        maxHP = Random.Range(20, 40);
        HP = maxHP;
        rawAttack = Random.Range(3, 10); ;
        rawDefense = Random.Range(1, 5); ;
        base.Setup();
    }

    public override async UniTask GoingAttack()
    {
        animation.Play("攻撃1");

        Attack(player.transform.position);
        // アニメーションの終了を待つ（基底のクラスの関数）
        // await WaitUntilAnimationStateExits(attackName); // ←"Attack"はアニメーターのステート名

    }

    protected override void HitEffect(Vector3 hitPos, float damage)
    {
        if (damage > HP / 3)
        {
            animation.Play("ダメージを受ける");
        }

        base.HitEffect(hitPos, damage);
    }
    public override void Dead()
    {
        base.Dead();
    }
}
