using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LongRange : EnemyCharacter
{

    public override void Setup()
    {
        attackStrategies = new Dictionary<AttackType, AttackStrategy>
        {
            { AttackType.LongRange, new LongRangeAttack() },
            { AttackType.TakeDistance, new TakeDistance() }
        };

        attackArea = 4;
        speed = 1;
        maxHP = 10;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        base.Setup();
    }

    [SerializeField]
    private Transform hand;
    public override async UniTask LongRangeAttack() {
        const int bulletCount = 10;
        const float interval = 0.5f;

        //成功したら、攻撃のチャージが完了するかどうか
        //if (!await ChargeTime(attackTime, attackName)) return;

        Attack(player.transform.position);

        for (int i = 0; i < bulletCount; i++) {
            if (this == null || player == null || hand == null) return;

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
}
