using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static ItemUtility;
using static CommonModule;
using System.Threading;

public class PlayerCharacter : CharacterBase
{
    private PlayerAction _playerAction;
    CancellationTokenSource cts = new CancellationTokenSource();
    //クリティカル率
    private int criticalRate = 5;
    //クリティカルダメージ率
    private int criticalDamageRate = 150;

    public override void Setup()
    {
        transform.SetParent(null);
        _playerAction = GetComponent<PlayerAction>();
        speed = 2.5f;
        maxHP = 100;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        possessItemList = new List<ItemBase>(_POSSESS_ITEM_MAX);

        //possessItemListに空を詰める
        for(int i = 0,max = _POSSESS_ITEM_MAX; i < max; i++) {
            possessItemList.Add(null);
        }
        //アイテムを返してもらう
        possessItemList = GetPlayerItems();
        //武器ももらう
        possessWeapon = GetPlayerWeapon();

        SetStatus();

        //自動回復とかの連続処理の呼び出し
        LoopTask(cts.Token).Forget();
        
    }
    private void Update()
    {
        if (transform.parent != null) return;
        //プレイヤーの操作の呼び出し
        _playerAction.AcceptInput();

        //座標の下限
        if(transform.position.y < -1) {
            SceneManager.LoadScene("Main");
        }

       
    }

    /// <summary>
    /// プレイヤーかどうか
    /// </summary>
    public override bool IsPlayer()
    {
        return true;
    }
    /// <summary>
    /// 死亡時処理
    /// </summary>
    public override void Dead()
    {
        // プレイヤー死亡でダンジョン終了
       // _EndDungeon?.Invoke(eDungeonEndReason.Dead);
    }

    public override UniTask GoingAttack()
    {   
        return UniTask.CompletedTask;
    }

    public override UniTask LongRangeAttack()
    {
        return UniTask.CompletedTask;
    }

    public override UniTask TakeDistance()
    {
        return UniTask.CompletedTask;
    }

    public override UniTask CounterAttack()
    {
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// アイテムを手に入れる時に呼ばれる処理
    /// </summary>
    public async void GetItem(ItemBase getItem) {
        //拾ったアイテムが武器だったら今持っているのと入れ替え
        if (getItem.isWeapon()) {
            //
            if(possessWeapon != null)
                RemoveItem(possessWeapon.itemID, transform.position);
            
            possessWeapon = getItem;

            return;
        }


        //アイテムだったら…
        //とりあえず先頭に置かせて！
        for(int i = 0,max = _POSSESS_ITEM_MAX; i < max; i++) {

            //アイテムリストがいっぱい
            if (IsFullList(possessItemList)) {
                await Menu.instance.SwapItemInMenu(getItem);
            }


            //アイテム枠にアイテムがあればスルー
            if (possessItemList[i] != null) continue;
            //アイテムゲット
            GetItem(getItem, i);

            return;
        }

    }

    /// <summary>
    /// 持っているアイテムの攻撃力をもらう
    /// </summary>
    /// <returns></returns>
    public float GetWeaponAttack() {
        //もしなんも持ってなかったら0を返してあげる
        if(possessWeapon == null) return 0f;

        return possessWeapon.GetComponent<ItemWeapon>().GetAttackValue();
    }

    /// <summary>
    /// 持っているアイテムの攻撃力をもらう
    /// </summary>
    /// <returns></returns>
    public float GetAccessaryAttack() {
        int AccessaryAttackSum = 0;
        for(int i = 0,max = _POSSESS_ITEM_MAX;i < max;i++) {
            if(possessItemList[i] == null) continue;
            
            //このis演算子はpossessItemList[i]がPowerUpItem型かどうかを検知してくれる
            if(possessItemList[i] is PowerUpItem)
                AccessaryAttackSum += (int)((PowerUpItem) possessItemList[i]).GetAttackValue();
        }
        return AccessaryAttackSum;
    }

    /// <summary>
    /// 他のところで保持しておいてもらったアイテム群を再び回収
    /// </summary>
    public void GetItemSlot(List<ItemBase> itemList, ItemBase weapon) {
        possessItemList = itemList;
        possessWeapon = weapon;
    }

    private void OnDisable() {
        SendItemList();
        // 止めたいとき
        cts.Cancel();
    }

    //アイテムリストを送る
    public void SendItemList() {
        //自身が壊されるタイミングでアイテムマネージャーにアイテムを渡す
        ItemManager.instance.SetPlayerItems(possessItemList, possessWeapon);
    }

    /// <summary>
    /// index指定でアイテムを捨てる
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItemFromList(int index) {

        if (possessItemList[index] == null) {
            Debug.Log("アイテムリストの" +index+"番目はありませんでした");
            return;
        }

        //アイテムを野に放つ
        RemoveItem(possessItemList[index].itemID, transform.position);
        possessItemList[index] = null;
    }

    public void GetItem(ItemBase getItem,int itemListIndex) {
        //アイテムをぶち込む
        possessItemList[itemListIndex] = getItem;
    }

    /// <summary>
    /// アイテムの回復力を使って回復
    /// </summary>
    public void Heal() {
        //アイテムリストが存在しなければおにまい
        if (possessItemList == null) return;
        //回復量
        int HealValue = 0;
        for (int i = 0, max = _POSSESS_ITEM_MAX; i < max; i++) {
            if (possessItemList[i] == null) continue;

            //このis演算子はpossessItemList[i]がHealItem型かどうかを検知してくれる
            if (possessItemList[i] is HealItem)
                HealValue += (int) ((HealItem) possessItemList[i]).GetHealValue();
        }

        

        if(HP+HealValue > maxHP) {
            HealValue = (int)(maxHP - HP);
        }

        //回復
        HP += HealValue;

        this.GetComponent<HPGaugeUI>().Heal(HealValue);
    }


    /// <summary>
    /// 一定時間に一回呼ばれる処理
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid LoopTask(CancellationToken token) {
        while (!token.IsCancellationRequested) {
            // 呼びたい処理
            
            Heal();

            // 3秒待つ
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
        }
    }

    /// <summary>
    /// クリティカル率
    /// </summary>
    /// <returns></returns>
    public int GetCritRate() {
         //回復量
        int CritValue = 0;
        for (int i = 0, max = _POSSESS_ITEM_MAX; i < max; i++) {
            if (possessItemList[i] == null) continue;

            //このis演算子はpossessItemList[i]がPowerUpItem型かどうかを検知してくれる
            if (possessItemList[i] is CritUpItem)
                CritValue += (int) ((CritUpItem) possessItemList[i]).GetCritUpValue();
        }

        return criticalRate + CritValue;
    }

    /// <summary>
    /// クリティカルダメージ率
    /// </summary>
    /// <returns></returns>
    public int GetCritDamageRate() {
        //クリティカルダメージ率
        int CritDamageValue = 0;
        for (int i = 0, max = _POSSESS_ITEM_MAX; i < max; i++) {
            if (possessItemList[i] == null) continue;

            //このis演算子はpossessItemList[i]がCritDamageUpItem型かどうかを検知してくれる
            if (possessItemList[i] is CritDamageUpItem)
                CritDamageValue += (int) ((CritDamageUpItem) possessItemList[i]).GetCritDamageUpValue();
        }

        return criticalDamageRate + CritDamageValue;
    }


}
