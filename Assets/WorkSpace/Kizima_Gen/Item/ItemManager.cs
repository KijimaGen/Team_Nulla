/*
 * @file ItemManager.cs
 * @brief アイテム主要処理
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour{
    
    public static ItemManager instance;
    //使用中のアイテム
    [SerializeField]
    Transform _useRoot;
    //未使用状態のアイテム
    [SerializeField]
    Transform _unuseRoot;
    //アイテムの枠
    [SerializeField]
    ItemBase originItem;

    //使用中のアイテム
    List<ItemBase> _useList = new List<ItemBase>();
    //未使用状態のアイテム
    List<ItemBase> _unuseList = new List<ItemBase>();

    const int _ITEM_MAX = 256;

    [SerializeField] List<GameObject> items = new List<GameObject>();

    private void Start() {
        Initialize();
    }


    private void Initialize() {
        instance = this;
        for(int i = 0; i < _ITEM_MAX; i++) {
            Instantiate(originItem, _unuseRoot);
            _useList.Add(originItem);
        }
        DebugScript.ActionTrigger();
    }

    /// <summary>
    /// アイテムの使用
    /// </summary>
    /// <param name="item"></param>
    public void UseItem() {
        ItemBase item = GetUsableItem();
        if (item != null) {
            //いちばん先頭にある使えるアイテムを使用する
            ItemBase instance = Instantiate(item); // ← ここが超大事！！
            instance.gameObject.transform.SetParent(_useRoot);
        }
    }

    /// <summary>
    /// アイテムを未使用状態にする
    /// </summary>
    /// <param name="item"></param>
    public void UnuseItem(int ID) {
        ItemBase item = _useList[ID];
        item.transform.SetParent(_unuseRoot);
    }

    /// <summary>
    /// 使用可能なアイテムを返す
    /// </summary>
    /// <returns></returns>
    private ItemBase GetUsableItem() {
        for(int i = 0,max =_useList.Count; i< max; i++) {
            if (_useList[i] != null) 
                return _useList[i];
            
        }
        return null;
    }

}
