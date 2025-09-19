using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LongRange : EnemyCharacter
{
    [SerializeField] protected GameObject prefabBullet;
    public override void Setup()
    {
        attackStrategies = new Dictionary<AttackType, AttackStrategy>
        {
            { AttackType.LongRange, new LongRangeAttack() },
            { AttackType.TakeDistance, new TakeDistance() }
        };

        attackArea = 4;
        speed = Random.Range(1, 3);
        maxHP = Random.Range(20,40);
        HP = maxHP;
        rawAttack = Random.Range(3, 10); ;
        rawDefense = Random.Range(1, 5); ;
        base.Setup();
    }

    [SerializeField]
    private Transform hand;
    public override async UniTask LongRangeAttack() {
        const int bulletCount = 10;
        const float interval = 0.5f;

        //成功したら、攻撃のチャージが完了するかどうか
        //if (!await ChargeTime(attackTime, attackName)) return;
        animation.Play("攻撃1");

        Attack(player.transform.position);

        for (int i = 0; i < bulletCount; i++) {
            if (this == null || player == null || hand == null) return;
            if (GetHitDamage()) return;
            Vector3 bulletRotation = (player.transform.position - hand.position).normalized;
            bulletRotation.x += Random.Range(-0.1f, 0.1f);
            bulletRotation.y += Random.Range(-0.1f, 0.1f);

            Instantiate(prefabBullet, hand.position, Quaternion.LookRotation(bulletRotation));

            AudioManager.instance.PlaySE(8);

            await UniTask.Delay((int)(interval * 1000));
        }


        // アニメーションの終了を待つ（基底のクラスの関数）
        // await WaitUntilAnimationStateExits(attackName); // ←"Attack"はアニメーターのステート名
    }
    protected override void HitEffect(Vector3 hitPos, float damage)
    {
        if (damage > HP / 3)
        {
            animation.Play("ダメージを受ける");
        }
            
        base.HitEffect(hitPos,damage);
    }
    public override void Dead()
    {
        base.Dead();
    }
}
