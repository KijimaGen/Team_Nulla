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
}
