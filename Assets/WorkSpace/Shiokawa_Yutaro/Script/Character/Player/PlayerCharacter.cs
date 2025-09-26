using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static ItemUtility;

public class PlayerCharacter : CharacterBase
{
    private PlayerAction _playerAction;
    


    public override void Setup()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        possessItemList = ItemManager.instance.GetPlayerItems();
        //武器ももらう
        possessWeapon = ItemManager.instance.GetPlayerWeapon();

        SetStatus();
    }
    private void Update()
    {
        if (transform.parent != null) return;
        //プレイヤーの操作の呼び出し
        _playerAction.AcceptInput();

        //座標の下限
        if(transform.position.y < -1) {
            //SceneManager.LoadScene("Main");
        }

        //座標の上限
        if (transform.position.y > 1.5) {

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
    public void GetItem(ItemBase getItem) {
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
            //アイテム枠にアイテムがあれば一旦スルー
            if (possessItemList[i] != null) continue;
            possessItemList[i] = getItem;

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




    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SendItemList();
    }
    //アイテムリストを送る
    public void SendItemList() {
        //自身が壊されるタイミングでアイテムマネージャーにアイテムを渡す
        ItemManager.instance.SetPlayerItems(possessItemList, possessWeapon);
    }
    /// <summary>
    /// ID指定でアイテムを捨てる
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItemFromList(int index)
    {
        //アイテムを野に放つ
        if (possessItemList[index] == null) return;
        RemoveItem(possessItemList[index].itemID, transform.position);
        possessItemList[index] = null;
    }
}
