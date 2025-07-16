/**
 * @file CharacterBase.cs
 * @brief キャラクター情報の基底
 * @author yao
 * @date 2025/5/8
 */

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class CharacterBase : MonoBehaviour
{
    public int ID { get; private set; } = -1;

    // マスターデータ依存の変数
    public int nameID { get; protected set; } = -1;
    public float maxHP { get; protected set; } = -1;
    public float HP { get; protected set; } = -1;
    // 死亡しているか
    public bool isDead { get { return HP <= 0; } }
    public float rawAttack { get; protected set; } = -1;
    public float rawDefense { get; protected set; } = -1;
    public float speed { get; protected set; } = -1;

    public List<int> possessItemList { get; protected set; } = null;
    //所持アイテムの最大数
    private static readonly int _POSSESS_ITEM_MAX = 6;

    protected Animator animator;
    protected Rigidbody rb;

    protected bool isAttacking = false;

    private void Start()
    {
        Setup();
    }

    /// <summary>
    /// 使用前の準備
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// マスターデータ関連の準備
    /// </summary>
    /// <param name="setMasterID"></param>
    protected void SetStatus()
    {
        SetMaxHP(maxHP);
        SetHP(HP);
        SetRawAttack(rawAttack);
        SetRawDefense(rawDefense);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }



    /// <summary>
    /// プレイヤーか否か
    /// </summary>
    /// <returns></returns>
    public abstract bool IsPlayer();


    /// <summary>
    /// フロア終了時処理
    /// </summary>
    //public virtual UniTask OnEndFloor()
    //{
    //    return UniTask.CompletedTask;
    //}

    /// <summary>
    /// 攻撃力取得
    /// </summary>
    /// <returns></returns>
    public float GetAttack()
    {
        return rawAttack;
    }

    /// <summary>
    /// 素の攻撃力設定
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetRawAttack(float setValue)
    {
        rawAttack = setValue;
    }

    /// <summary>
    /// 防御力取得
    /// </summary>
    /// <returns></returns>
    public float GetDefense()
    {
        return rawDefense;
    }

    /// <summary>
    /// 素の防御力設定
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetRawDefense(float setValue)
    {
        rawDefense = setValue;
    }

    /// <summary>
    /// 最大HP設定
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetMaxHP(float setValue)
    {
        maxHP = setValue;
    }

    /// <summary>
    /// 現在HP設定
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetHP(float setValue)
    {
        // 0～最大値に丸める
        HP = Mathf.Clamp(setValue, 0, maxHP);
    }

    /// <summary>
    /// HP回復
    /// </summary>
    /// <param name="addValue"></param>
    public void AddHP(float addValue)
    {
        SetHP(HP + addValue);
    }

    /// <summary>
    /// HP減少
    /// </summary>
    /// <param name="removeValue"></param>
    public void RemoveHP(float removeValue)
    {
        SetHP(HP - removeValue);
    }

    /// <summary>
    /// キャラクターの死亡
    /// </summary>
    public abstract void Dead();

    /// <summary>
    /// アニメーションの再生
    /// </summary>
    /// <param name="setAnim"></param>
    //public void SetAnimation(eCharacterAnimation setAnim)
    //{
    //    GetObject()?.SetAnimation(setAnim);
    //}

    /// <summary>
    /// 再生中のアニメーション取得
    /// </summary>
    /// <returns></returns>
    //public eCharacterAnimation GetCurrentAnimation()
    //{
    //    CharacterObject characterObject = GetObject();
    //    if (characterObject == null) return eCharacterAnimation.Invalid;

    //    return characterObject.currentAnim;
    //}

    /// <summary>
    /// 予定行動の実行
    /// </summary>
    /// <returns></returns>
    //public virtual async UniTask ExecuteScheduleAction()
    //{
    //    await UniTask.CompletedTask;
    //}

    /// <summary>
    /// 予定行動のリセット
    /// </summary>
    public virtual void ResetScheduleAction() { }

    /// <summary>
    /// ターン終了時の処理
    /// </summary>
    public virtual void OnEndTurn()
    {

    }

    public bool CanAddItem()
    {
        return possessItemList.Count < _POSSESS_ITEM_MAX;
    }

    public void AddItem(int addItemID)
    {
        possessItemList.Add(addItemID);
    }
    public void RemoveItem(int removeItemID)
    {
        possessItemList.Remove(removeItemID);
    }

    protected bool CheckGrounded(Vector3 dir)
    {
        Vector3 origin1 = (transform.position + dir * 2f);
        Vector3 origin2 = (transform.position + dir * 1.5f);
        Vector3 origin3 = (transform.position + dir * 1f);
        Vector3 origin4 = (transform.position + dir * 0.5f);
        Vector3 direction = Vector3.down;
        float rayLength = 2f;
        //int rayCount = 3;
        int groundLayer = LayerMask.GetMask("Ground");

        //for (int i = 0; i < rayCount; i++)
        //{
        //    Debug.DrawRay(origin, direction * rayLength, Color.red);
        //    return Physics.Raycast(origin , direction, rayLength, groundLayer);
        //}

        Debug.DrawRay(origin1, direction * rayLength, Color.red);
        Debug.DrawRay(origin2, direction * rayLength, Color.red);
        Debug.DrawRay(origin3, direction * rayLength, Color.red);

        return Physics.Raycast(origin1, direction, rayLength, groundLayer)
                 && Physics.Raycast(origin2, direction, rayLength, groundLayer)
                     && Physics.Raycast(origin3, direction, rayLength, groundLayer)
                         && Physics.Raycast(origin4, direction, rayLength, groundLayer);
    }

    protected void Attack(string attackName)
    {
       // rb.velocity = Vector3.zero;
        //animator.SetBool("run", false);
        //animator.SetTrigger("attack");
        //isAttacking = true;
        //playAnim = true;
    }

    public void IsAttackingOFF()
    {
        isAttacking = false;
    }
}

