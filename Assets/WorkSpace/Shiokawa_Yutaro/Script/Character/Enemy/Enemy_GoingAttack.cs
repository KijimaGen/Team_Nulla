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
        maxHP = 10;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        base.Setup();
    }
}
