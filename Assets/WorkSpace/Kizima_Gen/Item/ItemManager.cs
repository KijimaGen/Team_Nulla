/*
 * @file ItemManager.cs
 * @brief アイテム主要処理
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SystemObject{
    public static ItemManager instance;

    //アイテムを呼び出す先の参照
    [SerializeField] Transform _useRoot;
    [SerializeField] Transform _unuseRoot;
    [SerializeField] ItemBase originItem;

    [SerializeField] List<GameObject> items;

    //使用、不使用リスト
    List<ItemBase> _useList = new List<ItemBase>();
    List<ItemBase> _unuseList = new List<ItemBase>();

    //アイテムの最大数
    const int _ITEM_MAX = 256;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Initialize() {
        instance = this;
        for (int i = 0; i < _ITEM_MAX; i++) {
            ItemBase item = Instantiate(originItem, _unuseRoot);
            item.Initialize();
            
            _unuseList.Add(item);
        }
    }

    /// <summary>
    /// アイテムを使える状態にする
    /// </summary>
    public void UseItem() {
        ItemBase item = GetUsableItem();
        if (item != null) {
            _unuseList.Remove(item);
            _useList.Add(item);

            
            item.transform.SetParent(_useRoot);
           
        }
        else {
            Debug.LogWarning("使用可能なアイテムがありません！");
        }
    }

    /// <summary>
    /// アイテムを未使用状態にする
    /// </summary>
    /// <param name="ID"></param>
    public void UnuseItem(int ID) {
        if (_useList[0] ==  null) return;
        ItemBase item = _useList[ID];

        if (_useList.Contains(item)) {
            _useList.Remove(item);
            _unuseList.Add(item);

            item.transform.SetParent(_unuseRoot);
            
        }
    }

    /// <summary>
    /// 使用可能なアイテムを返す
    /// </summary>
    /// <returns></returns>
    private ItemBase GetUsableItem() {
        if (_unuseList.Count > 0) {
            return _unuseList[0]; // 先頭の未使用アイテムを返す
        }
        return null;
    }

    public void 
}