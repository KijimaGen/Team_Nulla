using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AttackType
{
    /// <summary>
    /// 攻撃を当てに行く
    /// </summary>
    Going,
    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    LongRange,
    /// <summary>
    /// 差し返し
    /// </summary>
    Counter,
    /// <summary>
    /// 間合いを取る
    /// </summary>
    TakeDistance,

    Max
}

public enum Action
{
    /// <summary>
    /// 待機
    /// </summary>
    Idel,
    /// <summary>
    /// 追う
    /// </summary>
    Chase,
    /// <summary>
    /// 攻撃する
    /// </summary>
    Attack,

    Max
}

public class EnemyState
{

    public class ChaseState : StateBase<EnemyCharacter>
    {
        public override async UniTask Enter(EnemyCharacter enemy)
        {
            //ここで追いかける前にプレイヤーを発見したアニメーションがほしい
            Debug.Log("追う状態を入った");
            await UniTask.CompletedTask;
        }

        public override async UniTask Execute(EnemyCharacter enemy)
        {
            Debug.Log("追いかける状態に入った");
            await enemy.StartChase();
        }

        public override async UniTask Exit(EnemyCharacter enemy)
        {
            Debug.Log("追う状態を抜けた");
            await UniTask.CompletedTask;
        }
    }

    public class AttackState : StateBase<EnemyCharacter>
    {
        public override async UniTask Enter(EnemyCharacter enemy)
        {
            Debug.Log("攻撃状態を入った");
            await UniTask.CompletedTask;
        }

        public override async UniTask Execute(EnemyCharacter enemy)
        {
            Debug.Log("攻撃をします");
            var availableTypes = enemy.attackStrategies.Keys.ToList();
            var randomType = availableTypes[Random.Range(0, availableTypes.Count)];

            await enemy.StartAttack((int)randomType);
        }

        public override async UniTask Exit(EnemyCharacter enemy)
        {
            Debug.Log("攻撃状態を抜けた");
            await UniTask.CompletedTask;
        }
    }

    public class IdelState : StateBase<EnemyCharacter>
    {
        public override async UniTask Enter(EnemyCharacter enemy)
        {
            Debug.Log("待機状態を抜けた");
            await UniTask.CompletedTask;
        }

        public override async UniTask Execute(EnemyCharacter enemy)
        {
            Debug.Log("待機状態を抜けた");
            await UniTask.CompletedTask;
        }

        public override async UniTask Exit(EnemyCharacter enemy)
        {
            Debug.Log("待機状態を抜けた");
            await UniTask.CompletedTask;
        }
    }
}
