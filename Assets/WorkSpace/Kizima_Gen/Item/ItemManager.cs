/*
 * @file ItemManager.cs
 * @brief アイテム主要処理
 * @author kijima
 * @date 2025/7/9
 */
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

    //デバッグ用のplayer
    private GameObject player;

    [SerializeField]
    private List<GameObject> itemRoots; 

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Initialize() {
        instance = this;
        for (int i = 0; i < _ITEM_MAX; i++) {
            ItemBase item = Instantiate(originItem, _unuseRoot);
            item.Initialize();
            
            //IDを指定して、アイテムを未使用状態にしておく
            item.SetItemID(i);
            _unuseList.Add(item);
        }
        player = GameObject.Find("Player");
        itemRoots = PlayerOpenChester.instance.GetItemRoots();
       
    }

    /// <summary>
    /// アイテムを使える状態にする
    /// </summary>
    public void UseItem(Vector3 spawnPos) {
        ItemBase item = GetUsableItem();
        if (item != null) {
            _unuseList.Remove(item);
            _useList.Add(item);

            
            item.transform.SetParent(_useRoot);
           item.transform.position = spawnPos;

            Debug.Log(item.gameObject.name +'['+ item.itemID+']' + "を使用します");
        }
        else {
            Debug.LogWarning("ああああああああああ");

        }
    }

    /// <summary>
    /// アイテムを未使用状態にする
    /// </summary>
    /// <param name="ID"></param>
    public void UnuseItem(int ID) {
        if (_useList[ID] ==  null) return;
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

    /// <summary>
    /// アイテムの取得
    /// </summary>
    /// <param name="ID"></param>
    public void GetItem(int ID) {
        ItemBase getItem = _useList.Find(item => item.itemID == ID);
        if (getItem == null) return;

        if (_useList.Contains(getItem)) {
            _useList.Remove(getItem);
            _unuseList.Add(getItem);

            for(int i = 0,max = itemRoots.Count; i < max; i++) {

                if (itemRoots[i].transform.childCount != 0) continue;
                getItem.transform.SetParent(itemRoots[i].transform);
                getItem.transform.position = itemRoots[i].transform.position;
                getItem.transform.rotation = itemRoots[i].transform.rotation;
                getItem.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                getItem.isPlayerPosses = true;
                Debug.Log(getItem.gameObject.name + getItem.itemID + "を獲得しました");

                return;
            }

            getItem.transform.SetParent(player.transform);
            getItem.transform.position = player.transform.position;
            getItem.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            getItem.isPlayerPosses = true;
            Debug.Log(getItem.gameObject.name + getItem.itemID + "を獲得しました");
        }

    }
}