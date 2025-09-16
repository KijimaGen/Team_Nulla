using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    private PlayerAction _playerAction;



    public override void Setup()
    {
        _playerAction = GetComponent<PlayerAction>();
        speed = 2.5f;
        maxHP = 10;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        possessItemList = new List<ItemBase>(_POSSESS_ITEM_MAX);

        //possessItemListに空を詰める
        for(int i = 0,max = _POSSESS_ITEM_MAX; i < max; i++) {
            possessItemList.Add(null);
        }

        SetStatus();
    }
    private void Update()
    {
        //プレイヤーの操作の呼び出し
        _playerAction.AcceptInput();
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
                ItemUtility.RemoveItem(possessWeapon.itemID, transform.position);
            
            possessWeapon = getItem;
            Debug.Log(getItem.name + "[" + getItem.itemID + "] を手に入れました");

            return;
        }


        //アイテムだったら…
        //とりあえず先頭に置かせて！
        for(int i = 0,max = _POSSESS_ITEM_MAX; i < max; i++) {
            //アイテム枠にアイテムがあれば一旦スルー
            if (possessItemList[i] != null) continue;
            possessItemList[i] = getItem;
            Debug.Log(getItem.name + "[" + getItem.itemID + "] を手に入れました");

            return;
        }

        Debug.Log("なんも手に入れられませんでした★");
    }
}
