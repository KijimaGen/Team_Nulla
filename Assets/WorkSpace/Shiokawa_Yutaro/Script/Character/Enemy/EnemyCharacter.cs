using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static EnemyState;
using Unity.VisualScripting;
using UnityEditor;
using System.Linq;
using UnityEngine.InputSystem.HID;


public class EnemyCharacter : CharacterBase
{
    /// <summary>
    /// エネミーの視野範囲
    /// </summary>
    protected static readonly float _ENEMY_VIEW_AREA = 15;

    Action actionCategory;
    bool onAction;
    float actionTime;

    [SerializeField] Transform neck;

    // ヒットエフェクト
    [SerializeField]
    private ParticleSystem hitEffect;

    protected PlayerCharacter player;

    protected float attackArea = 0.5f;
    bool action;

    private StateBase<EnemyCharacter> currentState;
    private StateBase<EnemyCharacter> nextState;

    private Dictionary<Action, StateBase<EnemyCharacter>> stateMap;
    public Dictionary<AttackType, AttackStrategy> attackStrategies;

    bool hitDamage;
    public DamageUI damageUI;

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

        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();

        SetStatus();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        //死んでたらリターン
        if (isDead) { animation.Play("死ぬ"); return; }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        }
        if (!ViewAction()) return;


        StateTick().Forget();
    }

    private Action previousAction = Action.Max;

    private async UniTask StateTick()
    {
        hitDamage = false;
        RotateTowardsPlayer();

        if (!ExecuteAction()) return;

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        Dictionary<Action, float> actionWeights = new();

        if (playerDistance < attackArea * 2)
        {
            // プレイヤーが近い：攻撃っぽい行動を強調
            actionWeights[Action.Attack] = 100f;
            actionWeights[Action.Chase] = 20f;
        }
        else
        {
            // プレイヤーが遠い：様子見や接近系が中心
            actionWeights[Action.Idel] = 50f;
            actionWeights[Action.Chase] = 40f;
        }

        // 同じ行動を避けながらランダム選出（重み付き）
        Action nextAction = GetRandomWeightedAction(actionWeights, previousAction);
        //Action nextAction = Action.Chase;

        previousAction = nextAction;
        nextState = stateMap[nextAction];
        await SetNextState(nextState);
    }

    public async UniTask SetNextState(StateBase<EnemyCharacter> nextState)
    {
        if (currentState != null)
        {
            await currentState.Exit(this);
        }

        currentState = nextState;

        if (currentState != null)
        {
            await currentState.Enter(this);
            await currentState.Execute(this);
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
        float actionInterval = UnityEngine.Random.Range(2, 4);
        if (actionTime >= actionInterval)
        {
            actionTime = 0;
            action = true;
            return true;
        }
        return false;
    }

    bool playerFound = false;
    float playerSeeTime = 0f;
    /// <summary>
    /// プレイヤーを見つけるかの処理
    /// </summary>
    /// <returns></returns>
    protected virtual bool ViewAction()
    {
        Vector3 neckPos = neck.position;
        Vector3 viewPos = new Vector3(neckPos.x, neckPos.y, neckPos.z);
        float viewAngle = 240;
        float halfAngle = viewAngle / 2f;
        int rayCount = 30;



        for (int i = 0; i < rayCount; i++)
        {
            float t = (float)i / (rayCount - 1);
            float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 dir = rotation * transform.forward;

            Ray ray = new Ray(viewPos, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, _ENEMY_VIEW_AREA))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    float dist = Vector3.Distance(neckPos, hit.transform.position);
                    if (dist <= _ENEMY_VIEW_AREA)
                    {
                        playerSeeTime = 0;
                        playerFound = true;
                        break;
                    }
                }
                else
                {
                    playerSeeTime += Time.deltaTime;
                    if (playerSeeTime >= 10)
                    {
                        playerFound = false;
                        playerSeeTime = 0;
                    }
                }
            }
            if (playerFound) break;
        }

        if (playerFound)
        {
            return true; // プレイヤーが視界内
        }
        else
        {
            Wandering(); // プレイヤーがいなければ徘徊
            return false;
        }
    }


    Vector3 randomTarget;
    float wanderCooldown = 0f;
    bool hasTarget = false;
    void Wandering()
    {
        // ターゲット更新
        wanderCooldown -= Time.deltaTime;
        if (wanderCooldown <= 0f || !hasTarget)
        {
            wanderCooldown = Random.Range(2f, 5f); // 次に動き出すまでの間隔
            float radius = 7f;
            Vector2 randomOffset = Random.insideUnitCircle * radius;
            randomTarget = new Vector3(transform.position.x + randomOffset.x, transform.position.y, transform.position.z + randomOffset.y);
            hasTarget = true;
        }

        // 移動処理
        Vector3 dir = randomTarget - transform.position;
        dir.y = 0f;

        if (dir.magnitude > 0.5f) // 目的地まで距離がある
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
            rb.velocity = transform.forward * speed;
            animation.Play("歩く");
        }
        else
        {
            rb.velocity = Vector3.zero;
            animation.Play("待機");
            hasTarget = false; // 次の wanderCooldown で新しいターゲットを決める
        }
    }


    /// <summary>
    /// 追いかける
    /// </summary>
    /// <returns></returns>
    public async UniTask StartChase()
    {
        while (true)
        {
            // プレイヤーが消えていたらループ終了
            if (player == null)
            {
                Debug.Log("プレイヤーが消えたので追跡終了");
                return;
            }
            Vector3 dir = (player.transform.position - transform.position).normalized;
            // プレイヤーとの距離チェック
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 flatTargetPos = player.transform.position;
            flatTargetPos.y = transform.position.y; // 高さを合わせる

            transform.DOLookAt(flatTargetPos, 0.3f);

            if (distance < attackArea)
            {
                // 攻撃に移るなど
                await SetNextState(new AttackState());
                break;
            }
            else if (distance > 2)
            {
                rb.velocity = dir * speed * 4;
                animation.Play("ダッシュ");
            }
            else
            {
                rb.velocity = dir * speed * 1;
                animation.Play("歩く");
            }

            // 0.1秒ごとにチェック（負荷軽減）
            await UniTask.Delay(100);
        }
    }
    #region 攻撃関係

    private AttackType lastAttackType;
    /// <summary>
    /// 攻撃をする範囲と攻撃の実行
    /// </summary>
    /// <returns></returns>
    public async UniTask StartAttack(int attackType)
    {
        if (hitDamage) return;
        var selectedType = (AttackType)attackType;

        // もし同じ攻撃タイプだったら通常攻撃に強制変更
        if (lastAttackType == selectedType)
        {
            Debug.Log("同じ攻撃だったので当てに行く攻撃に切り替え");
            selectedType = AttackType.Going;
        }

        rb.velocity = Vector3.zero;
        //selectedType = AttackType.Counter;
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

    public override async UniTask GoingAttack()
    {
        //ここの文の書き方がきもいからなんか変えたい
        const float attackTime = 2;
        const string attackName = "攻撃当てる";

        //成功したら、攻撃のチャージが完了するかどうか
        if (!await ChargeTime(attackTime, attackName)) return;
        // プレイヤーがぎりかわせる攻撃の実行
        Attack(player.transform.position);

        // アニメーションの終了を待つ（基底のクラスの関数）
        // await WaitUntilAnimationStateExits(attackName); // ←"Attack"はアニメーターのステート名

    }

    public override async UniTask LongRangeAttack()
    {
        const int bulletCount = 10;
        const float interval = 0.5f;

        //成功したら、攻撃のチャージが完了するかどうか
        //if (!await ChargeTime(attackTime, attackName)) return;

        Attack(player.transform.position);


        // アニメーションの終了を待つ（基底のクラスの関数）
        // await WaitUntilAnimationStateExits(attackName); // ←"Attack"はアニメーターのステート名
    }

    public override UniTask CounterAttack()
    {
        return UniTask.CompletedTask;
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
    public override async UniTask TakeDistance()
    {

        //プレイヤーから一定の距離を取る処理（ワンちゃん崖に落ちのでどうしよう）
        //崖に落ちるくらいなら地面の中心に戻す×

        //後ろの距離を取る地点の取得
        Vector3 fallPoint = transform.localPosition + -transform.forward * 4f;

        if (!CheckGrounded(fallPoint / 2))
        {
            //Debug.Log("後ろには飛べない");
            return;
        }


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

    bool onDead;
    /// <summary>
    /// 死亡時処理
    /// </summary>
    public override void Dead()
    {
        rb.velocity = -transform.forward * 2 + Vector3.up * 2;
        Destroy(gameObject);
    }

    public bool GetHitDamage()
    {
        return hitDamage;
    }
    public void SetHitDamage(bool set)
    {
        hitDamage = set;
    }

    /// <summary>
    /// ダメージ処理が内包されているよ～
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {

            PlayerCharacter player = other.transform.root.GetComponent<PlayerCharacter>();
            PlayerAction playerAction = other.transform.root.GetComponent<PlayerAction>();
            if (player == null) return;
            if (player.isAttacking)
            {
                playerFound = true;

                //ダメージ計算
                float damage = player.rawAttack + player.GetWeaponAttack() + player.GetAccessaryAttack();

                //さすがに0ダメージは可愛そうだと思う
                if (damage <= 0) {
                    damage = 1;
                }

                //ダメージが二倍にならないかな～
                damage = CommonModule.CalcClit((int) damage, player.GetCritRate(),player.GetCritDamageRate());

                //乱数を作成
                damage += Random.Range(-3, 4);

                Damage(this.GetComponent<Collider>(), (int)damage);

                HP -= damage;
                playerAction.AddHitPoint(damage * 0.01f);

                

                AudioManager.instance.PlaySE(0);
                HitEffect(other.transform.position, damage);
            }
        }
    }

    private float lastHitTime = -999f;
    private float hitInterval = 0.5f; // 0.5秒に1回ダメージ

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "必殺エフェクト片手剣(Clone)")
        {
            if (Time.time - lastHitTime > hitInterval)
            {
                lastHitTime = Time.time;

                playerFound = true;

                float damage = player.rawAttack + player.GetWeaponAttack() + player.GetAccessaryAttack();

                damage += Random.Range(-2, 3);
                if (damage <= 0) damage = 1;

                Damage(this.GetComponent<Collider>(), (int)damage);
                HP -= damage;

                AudioManager.instance.PlaySE(0);
                HitEffect(other.transform.position, damage);
            }
        }
    }

    public void Damage(Collider collider, int damage)
    {

        DamageUI DUI = damageUI;
        if (DUI != null)
        {
            DamageUI createObject = Instantiate(DUI, collider.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
            // 計算

            // テキストに表示

            createObject.ChangeDamageText(damage.ToString());
        }
    }

    protected virtual void HitEffect(Vector3 hitPos, float damage)
    {
        hitDamage = true;
        ParticleSystem hitEffectClone = Instantiate(hitEffect, hitPos, Quaternion.identity);
        Destroy(hitEffectClone.gameObject, 2);

        if (damage > HP / 3)
        {
            //ノックバックを与える
            rb.velocity = Vector3.zero;
            Vector3 playerDir = player.transform.position - transform.position;
            rb.velocity = Vector3.up + -playerDir * 2;
        }

    }

}
