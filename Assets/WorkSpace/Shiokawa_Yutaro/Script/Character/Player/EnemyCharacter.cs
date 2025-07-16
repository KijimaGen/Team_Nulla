using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCharacter : CharacterBase
{
    enum Action
    {
        Invalid = -1,
        /// <summary>
        /// 待機
        /// </summary>
        Idel,
        /// <summary>
        /// 索敵
        /// </summary>
        Search,
        
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

    Action actionCategory;
    bool onAction;
    float actionTime;

    [SerializeField] Transform neck;

    PlayerCharacter player;

    enum AttackType
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
        /// 差し返し(通常エネミーはいらないBossだけ)
        /// </summary>
        Counter,
        /// <summary>
        /// 間合いを取る
        /// </summary>
        TakeDistance,

        Max
    }

    public override void Setup()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        speed = 1;
        maxHP = 10;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        SetStatus();
    }
    // Update is called once per frame
    async void Update()
    {
        ActionProcess(ViewAction());
        
        if(actionCategory == Action.Chase)
        {
            await TargetChase();
        }
    }

    private Transform ViewAction()
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
                        return null;
                    }

                    return hit.transform;
                }
            }
            Debug.DrawRay(viewPos, dir * rayDistance, Color.red);
        }

        return null;
    }
    private async UniTask TargetChase()
    {
        // プレイヤーを追い続ける
        Vector3 chaseDir = player.transform.position - transform.position;
        chaseDir.y = 0f;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(chaseDir),
            Time.deltaTime * 5f
        );

        rb.velocity = transform.forward * speed;

        await UniTask.CompletedTask;
    }
    protected virtual async void ActionProcess(Transform targetPlayer)
    {
        //首を動かす
       // RotateTowardsPlayer();
        //アクションを実行するかどうか [実行しないならリターン]
        if (!ExecuteAction()) return;

        if(targetPlayer == null)
        {
            actionCategory = (Action)UnityEngine.Random.Range((int)Action.Idel,(int)Action.Chase);
        }
        else
        {
            actionCategory = (Action)UnityEngine.Random.Range((int)Action.Chase, (int)Action.Max);
            //actionCategory = (Action)2;
        }
        //ランダムでアクションを発動させる（もう少し確率をいじったほうがいい）
        //actionCategory = (Action)UnityEngine.Random.Range(0, (int)Action.Max);
        //actionCategory = ;
        switch (actionCategory)
        {
            case Action.Idel:
                Debug.Log("待機");
                await StartIdel();
                break;
            case Action.Search:
                Debug.Log("索敵する");
                //ここで索敵のアニメーションを行く。プレイヤーを見つける処理は常に別で動いてる
                await StartSearch();
                break;         

            //---------ここから下はプレイヤーがいないと処理されない-----------
            case Action.Chase:
                Debug.Log("追う");
                await StartChase();
                break;
            case Action.Attack:
                Debug.Log("攻撃発動");
                await StartAttack(UnityEngine.Random.Range(0, (int)AttackType.Max));
                break;
        }

        //アクションが終わったら
        onAction = false;


        //必要なこと状態を意識したステートベース繰り返す
        //
    }
    /// <summary>
    /// アクションを実行する時間
    /// </summary>
    /// <returns></returns>
    private bool ExecuteAction()
    {
        if (onAction || isAttacking) return false;

        actionTime += Time.deltaTime;
        float actionInterval = UnityEngine.Random.Range(2, 7);
        if (actionTime >= actionInterval)
        {
            actionTime = 0;
            onAction = true;
            return true;
        }
        return false;
    }
    /// <summary>
    /// 待機状態になって休憩するようにしたいなぁ（モウハンみたいに）
    /// </summary>
    /// <returns></returns>
    private async UniTask StartIdel()
    {
        rb.velocity = Vector3.zero;
    }
    /// <summary>
    /// 徘徊する
    /// </summary>
    private async UniTask StartSearch()
    {
       
    }
    private async UniTask StartChase()
    {
        int area = 5;
        if (CheckInAttackArea(area))
        {
            Debug.Log("近いので追わない");
            actionCategory = Action.Invalid;
            rb.velocity = Vector3.zero;
            return;
        }
        //アニメーション
    }
   

    #region 攻撃関係

    /// <summary>
    /// 攻撃をする範囲と攻撃の実行
    /// </summary>
    /// <returns></returns>
    public async UniTask StartAttack(int attackType)
    {
        isAttacking = true;
        switch ((AttackType)attackType)
        {
            case AttackType.Going:
                Debug.Log("攻撃しに行く");
                await GoingAttack();
                break;
            case AttackType.LongRange:
                Debug.Log("遠距離攻撃");
                await LongRangeAttack();
                break;
            case AttackType.TakeDistance:
                Debug.Log("間合いを取る");
                await StartTakeDistance();
                break;
        }

    }

    protected virtual async UniTask GoingAttack()
    {
        float attackArea = 3;
        if (CheckInAttackArea(attackArea)) return;
        //ここの文の書き方がきもいからなんか変えたい
        const float attackTime = 3;
        const string attackName = "攻撃しに行く";

        rb.DOMove(player.transform.position, 1);
        //攻撃のチャージが完了するかどうか
        if (await ChargeTime(attackTime)) return;
        // 攻撃の実行
        Attack(attackName);
    }

    protected virtual async UniTask LongRangeAttack()
    {
        float attackArea = 10;
        if (CheckInAttackArea(attackArea)) return;
        //ここの文の書き方がきもいからなんか変えたい
        const float attackTime = 3;
        const string attackName = "攻撃遠距離";

        rb.velocity  = Vector3.zero;
        //攻撃のチャージが完了するかどうか
        if (await ChargeTime(attackTime)) return;
        // 攻撃の実行
        Attack(attackName);
    }
    /// <summary>
    /// プレイヤーが近づいて攻撃してくるのを攻撃する
    /// </summary>
    /// <returns></returns>
    protected virtual async UniTask CounterAttack()
    {
        //通常エネミーにカウンターは追加しない
    }

    /// <summary>
    /// 後ろに回避して間合いを取る
    /// </summary>
    /// <returns></returns>
    private async UniTask StartTakeDistance()
    {

        //プレイヤーから一定の距離を取る処理（ワンちゃん崖に落ちのでどうしよう）
        //崖に落ちるくらいなら地面の中心に戻す

        //後ろの距離を取る地点の取得
        Vector3 fallPoint = transform.localPosition + -transform.forward * 4f;

        if (!CheckGrounded(fallPoint / 2))
        {
            Debug.Log("後ろには飛べない");
            return;
        }

        //animator.SetTrigger("takeDistance");

        //// 400ms待つ
        //await UniTask.Delay(400);

        ////個々の瞬間だけ一瞬重くなる
        //transform.DOLocalMove(fallPoint, 1f);

    }
    /// <summary>
    /// 攻撃時間とチャージ画像
    /// </summary>
    /// <param name="time"></param>
    /// <param name="warningLineName"></param>
    /// <returns></returns>
    protected virtual async UniTask<bool> ChargeTime(float time)
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
    /// <summary>
    /// 攻撃範囲内かどうか
    /// </summary>
    /// <param name="attackArea"></param>
    /// <returns></returns>
    private bool CheckInAttackArea(float attackArea)
    {
        //プレイヤーが近くにいなければ攻撃しない
        if (Vector3.Distance(transform.position, player.transform.position) >= attackArea) { onAction = false; return false; }
        return true;
    }

    #endregion

    /// <summary>
    /// 首をプレイヤーに向かせる
    /// </summary>
    private void RotateTowardsPlayer()
    {
        if (actionCategory == Action.Attack || actionCategory == Action.Search) return;
        Vector3 targetDir = player.transform.position - transform.position;

        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(forward, targetDir, Vector3.up);

        //首の回る最大数
        float maxHorizontalNeckAngle = 40;
        float maxVerticalNeckAngle = 40;
        if (Mathf.Abs(angle) <= maxHorizontalNeckAngle)
        {
            // 首がプレイヤーを向く処理（上下あり）
            Vector3 lookDir = player.transform.position - neck.position;
            Quaternion targetRot = Quaternion.LookRotation(lookDir);

            // 上下角チェック（首のローカルX軸回転）
            Quaternion localTargetRot = Quaternion.Inverse(transform.rotation) * targetRot;
            float verticalAngle = localTargetRot.eulerAngles.x;
            if (verticalAngle > 180) verticalAngle -= 360; // -180〜180 に変換

            if (Mathf.Abs(verticalAngle) <= maxVerticalNeckAngle)
            {
                neck.transform.rotation = targetRot;
            }
            else
            {
                // 上下の角度が大きすぎるので元に戻す
                Quaternion defaultNeckRotation = Quaternion.Euler(new Vector3(-83, -64, 24));
                neck.transform.localRotation = Quaternion.Slerp(neck.transform.localRotation, defaultNeckRotation, Time.deltaTime * 5f);
            }
        }
        else
        {
            // 首の範囲を超えたら体をゆっくり回す（水平のみ）
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            Vector3 euler = targetRot.eulerAngles;
            euler.x = 0f; // 上下回転を消す
            euler.z = 0f;

            Quaternion horizontalRot = Quaternion.Euler(euler);
            transform.rotation = Quaternion.Slerp(transform.rotation, horizontalRot, Time.deltaTime * 3f);
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
