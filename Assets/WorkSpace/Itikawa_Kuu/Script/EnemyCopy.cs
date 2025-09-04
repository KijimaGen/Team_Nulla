using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static EnemyState;
using Unity.VisualScripting;
using UnityEditor;
using System.Linq;

public class EnemyCopy : CharacterBase
{

    Action actionCategory;
    bool onAction;
    float actionTime;

    [SerializeField] Transform neck;

    PlayerCharacter player;

    float attackArea = 0.5f;
    bool action;

    public static StateBase<EnemyCharacter> currentState;
    private StateBase<EnemyCharacter> nextState;

    public static Dictionary<Action, StateBase<EnemyCharacter>> stateMap;
    private Dictionary<AttackType, AttackStrategy> attackStrategies;

    public override void Setup()
    {
        // ステート登録
        stateMap = new Dictionary<Action, StateBase<EnemyCharacter>>
        {
            { Action.Idel, new IdelState() },
            { Action.Chase, new ChaseState() },
            { Action.Attack, new AttackState() },
        };

        currentState = stateMap[Action.Idel];

        attackStrategies = new Dictionary<AttackType, AttackStrategy>
        {
            { AttackType.Going, new GoingAttack() },
            { AttackType.LongRange, new LongRangeAttack() },
            { AttackType.Counter, new CounterAttack() },
            { AttackType.TakeDistance, new TakeDistanceState() }
        };

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        speed = 1;
        maxHP = 10;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        SetStatus();
    }
    // Update is called once per frame
    void Update()
    {
        //死んでたらリターン
        if (isDead) return;
        if (!ViewAction()) return;

        StateTick().Forget();

        //HPバー追加
    }

    private Action previousAction = Action.Max;

    private async UniTask StateTick()
    {
        RotateTowardsPlayer();

        if (!ExecuteAction()) return;

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        Dictionary<Action, float> actionWeights = new();

        if (playerDistance < 6f)
        {
            // プレイヤーが近い：攻撃っぽい行動を強調
            actionWeights[Action.Attack] = 50f;
            actionWeights[Action.Chase] = 30f;
        }
        else
        {
            // プレイヤーが遠い：様子見や接近系が中心
            actionWeights[Action.Idel] = 50f;
            actionWeights[Action.Chase] = 40f;
        }

        // 同じ行動を避けながらランダム選出（重み付き）
        //Action nextAction = GetRandomWeightedAction(actionWeights, previousAction);
        Action nextAction = Action.Chase;

        previousAction = nextAction;
        nextState = stateMap[nextAction];
        await SetNextState(nextState);
    }

    public async UniTask SetNextState(StateBase<EnemyCharacter> nextState)
    {
        if (currentState != null)
        {
            //await currentState.Exit(this);
        }

        currentState = nextState;

        if (currentState != null)
        {
           //await currentState.Enter(this);
           //await currentState.Execute(this);
        }

        action = false;
    }

    private Action GetRandomWeightedAction(Dictionary<Action, float> weights, Action exclude)
    {
        // 1. 前回と同じ行動は除く
        var filteredWeights = weights
            .Where(kvp => kvp.Key != exclude)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // 2. 重みの合計を出す
        float totalWeight = filteredWeights.Values.Sum();

        // 3. 0〜合計の間でランダムな数を生成
        float rand = UnityEngine.Random.Range(0f, totalWeight);
        Debug.Log(rand + "F");
        // 4. ランダム値を超えるまで累計していって、一致したところを選ぶ
        float cumulative = 0f;

        foreach (var kvp in filteredWeights)
        {
            cumulative += kvp.Value;
            if (rand <= cumulative)
                return kvp.Key;
        }

        // 念のため（通常ここに来ることはない）
        return filteredWeights.Keys.First();
    }


    /// <summary>
    /// アクションを実行する時間
    /// </summary>
    /// <returns></returns>
    private bool ExecuteAction()
    {

        if (action) return false;

        actionTime += Time.deltaTime;
        float actionInterval = UnityEngine.Random.Range(3, 6);
        if (actionTime >= actionInterval)
        {
            actionTime = 0;
            action = true;
            return true;
        }
        return false;
    }
    /// <summary>
    /// プレイヤーを見つけるかの処理
    /// </summary>
    /// <returns></returns>
    private bool ViewAction()
    {
        Vector3 neckPos = neck.position;
        Vector3 viewPos = new Vector3(neckPos.x, neckPos.y, neckPos.z);
        float viewAngle = 120;
        float halfAngle = viewAngle / 2f;
        int rayCount = 30;
        float rayDistance = 5;

        for (int i = 0; i < rayCount; i++)
        {
            float t = (float)i / (rayCount - 1);
            float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 dir = rotation * transform.forward;

            Ray ray = new Ray(viewPos, dir);
            RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Player"))
                {                   

                    float dist = Vector3.Distance(neckPos, hit.transform.position);
                    if (dist > rayDistance)
                    {
                        return false;
                    }

                    else return true;
                }
            }
            Debug.DrawRay(viewPos, dir * rayDistance, Color.red);
        }

        return false;
    }
    /// <summary>
    /// 追いかける
    /// </summary>
    /// <returns></returns>
    public async UniTask StartChase()
    {
        while (true)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            // プレイヤーとの距離チェック
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log($"プレイヤーとの距離: {distance}");

            if (distance < attackArea)
            {
                // 攻撃に移るなど
                await SetNextState(new AttackState());
                break;
            }
            else
            {
                rb.velocity = dir * speed * 2;
            }

            // 0.1秒ごとにチェック（負荷軽減）
            await UniTask.Delay(100);
        }
    }
    #region 攻撃関係

    /// <summary>
    /// 攻撃をする範囲と攻撃の実行
    /// </summary>
    /// <returns></returns>
    private AttackType lastAttackType;

    public async UniTask StartAttack(int attackType)
    {
        var selectedType = (AttackType)attackType;

        // もし同じ攻撃タイプだったら通常攻撃に強制変更
        if (lastAttackType == selectedType)
        {
            Debug.Log("同じ攻撃だったので当てに行く攻撃に切り替え");
            selectedType = AttackType.Going;
        }

        rb.velocity = Vector3.zero;
        selectedType = AttackType.Counter;
        if (attackStrategies.TryGetValue(selectedType, out var strategy))
        {
            lastAttackType = selectedType;
            await strategy.Execute(this);
        }
        else
        {
            Debug.LogWarning($"未定義の攻撃タイプ: {selectedType}");
        }
    }

    public async UniTask GoingAttack()
    {
        //ここの文の書き方がきもいからなんか変えたい
        const float attackTime = 2;
        const string attackName = "攻撃当てる";

        //成功したら、攻撃のチャージが完了するかどうか
        if (!await ChargeTime(attackTime, attackName)) return;
        // プレイヤーがぎりかわせる攻撃の実行
        Attack(attackName);

        // アニメーションの終了を待つ（基底のクラスの関数）
       // await WaitUntilAnimationStateExits(attackName); // ←"Attack"はアニメーターのステート名

    }

    public async UniTask LongRangeAttack()
    {
        //ここの文の書き方がきもいからなんか変えたい
        const float attackTime = 2;
        const string attackName = "攻撃遠距離";

        //成功したら、攻撃のチャージが完了するかどうか
        if (!await ChargeTime(attackTime, attackName)) return;
        // プレイヤーがぎりかわせる攻撃の実行
        Attack(attackName);

        // アニメーションの終了を待つ（基底のクラスの関数）
       // await WaitUntilAnimationStateExits(attackName); // ←"Attack"はアニメーターのステート名
    }
    /// <summary>
    /// 攻撃時間とチャージ画像
    /// </summary>
    /// <param name="time"></param>
    /// <param name="warningLineName"></param>
    /// <returns></returns>
    private async UniTask<bool> ChargeTime(float time, string warningLineName)
    {
        float currentChargeTime = 0f;

        //攻撃のチャージ時間
        while (currentChargeTime <= time)
        {
            currentChargeTime += Time.deltaTime;
            await UniTask.DelayFrame(1);
        }

        return true;
    }
    public async UniTask StartTakeDistance()
    {

        //プレイヤーから一定の距離を取る処理（ワンちゃん崖に落ちのでどうしよう）
        //崖に落ちるくらいなら地面の中心に戻す×

        //後ろの距離を取る地点の取得
        Vector3 fallPoint = transform.localPosition + -transform.forward * 4f;

        if (!CheckGrounded(fallPoint / 2))
        {
            Debug.Log("後ろには飛べない");
            return;
        }

        animator.SetTrigger("takeDistance");

        // 400ms待つ
        await UniTask.Delay(400);

        //個々の瞬間だけ一瞬重くなる
        transform.DOLocalMove(fallPoint, 1f);

    }
    #endregion
    /// <summary>
    /// 首をプレイヤーに向かせる
    /// </summary>
    public void RotateTowardsPlayer()
    {
        if (actionCategory == Action.Attack) return;
        Vector3 targetDir = player.transform.position - transform.position;
        targetDir.y = 0f; // 水平方向のみに限定

        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(forward, targetDir, Vector3.up);

        //首の回る最大数
        float maxAngle = 40;
        if (Mathf.Abs(angle) <= maxAngle)
        {

            Vector3 lookTarget = player.transform.position + Vector3.up * 0.5f;
            neck.transform.rotation = Quaternion.LookRotation(lookTarget - neck.position);

        }
        else
        {
            // 首の範囲を超えたら体をゆっくり回す
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 3f); // ←回転速度
        }
    }

    private Vector3 PickRandomDirection()
    {
        float radius = 7f;
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * radius;
        return new Vector3(transform.position.x + randomOffset.x, transform.position.y, transform.position.z + randomOffset.y);
    }
    /// <summary>
    /// プレイヤーかどうか
    /// </summary>
    public override bool IsPlayer()
    {
        return false;
    }
    /// <summary>
    /// 死亡時処理
    /// </summary>
    public override void Dead()
    {
    }
}
