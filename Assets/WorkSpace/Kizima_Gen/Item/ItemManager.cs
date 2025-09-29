/*
 * @file ItemManager.cs
 * @brief アイテム主要処理
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : SystemObject{
    public static ItemManager instance;

    //アイテムを呼び出す先の参照
    [SerializeField] Transform _useRoot;
    [SerializeField] Transform _unuseRoot;
    

   
    [SerializeField] List<ItemBase> originItemList;

    //使用、不使用リスト
    List<ItemBase> _useList = new List<ItemBase>();
    List<ItemBase> _unuseList = new List<ItemBase>();

    //アイテムの最大数
    const int _ITEM_MAX = 256;

    //player
    private GameObject player;
    //プレイヤーのアクセサリーリスト
    private List<ItemBase> playerItemList = new List<ItemBase>(5); 
    //プレイヤーの所持アイテム
    private ItemBase playerWeapon;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Initialize() {
        instance = this;
        for (int i = 0; i < _ITEM_MAX; i++) {
            ItemBase item = Instantiate(originItemList[Random.Range(0,originItemList.Count)], _unuseRoot);
            item.Initialize();
            
            //IDを指定して、アイテムを未使用状態にしておく
            item.SetItemID(i);
            _unuseList.Add(item);
        }

        Reset();

        _useList.RemoveAll(item => item == null || item.Equals(null));
        //シーン遷移しても壊れない
        DontDestroyOnLoad(this.gameObject);

        //シーンマネージャーにシーン遷移したときに処理を呼んでもらう 
        SceneManager.sceneLoaded += OnSceneLoaded;

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
        if (GetItemFromList(ID) == null) return;
        ItemBase item = GetItemFromList(ID);

        if (_useList.Contains(item)) {
            _useList.Remove(item);
            _unuseList.Add(item);

            Debug.Log($"item = {item}, destroyed? {(item == null ? "yes" : "no")}");
            Debug.Log($"unuseRoot = {_unuseRoot}, destroyed? {(_unuseRoot == null ? "yes" : "no")}");

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
        //ここで一旦デストロイしてあるアイテムをなくしておく
        _useList.RemoveAll(item => item == null || item.Equals(null));
        

        ItemBase getItem = _useList.Find(item => item.itemID == ID);
        if (getItem == null) return;

        if (_useList.Contains(getItem)) {
            
            //プレイヤーにアイテムを渡す
            player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerCharacter>().GetItem(GetItemFromList(ID));
            getItem.isPlayerPosses = true;
            getItem.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// プレイヤーがアイテムを持っているかどうかを返す
    /// </summary>
    /// <returns></returns>
    public bool PlayerHasItem() {
        //for分を回してその中にisPlayerPossesがtrueなアイテムがあるかを返す
        for(int i = 0,max  = _unuseList.Count; i < max; i++) {
            if (_unuseList[i].isPlayerPosses)
                return true;
        }
        //for分を抜ける=そんなアイテムはないのでfalseを返す
        return false;

    }

    /// <summary>
    /// プレイヤーが持っているアイテムの数
    /// </summary>
    /// <returns></returns>
    public int GetHasPlayerItemCount() {
        int count = 0;
        //for分を回してその中にisPlayerPossesがtrueなアイテムがあるかを返す
        for (int i = 0, max = _unuseList.Count; i < max; i++) {
            if (_unuseList[i].isPlayerPosses)
                count++;
        }
        return count;
    }

    /// <summary>
    /// ID、座標指定でアイテムを野に放つ
    /// </summary>
    /// <param name="ID"></param>
    public void RemoveItem(int ID,Vector3 removePos) {

        //ここでY軸加算して埋まらないようにする
        removePos.y += 0.1f;

        _useList[ID].gameObject.transform.position = removePos;
        _useList[ID].isPlayerPosses = false;
        _useList[ID].gameObject.SetActive(true);
    }


    /// <summary>
    /// ID指定でアイテムを引き渡す
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private ItemBase GetItemFromList(int ID) {
        //使用中のアイテムリストからIDが一致するものを返す
        for(int i = 0,max  = _useList.Count; i < max; i++) {
            if (_useList[i].itemID == ID)
                return _useList[i];
        }
        return null;
    }

    /// <summary>
    /// プレイヤーの持っているアイテムを保持する
    /// </summary>
    public void SetPlayerItems(List<ItemBase> itemList,ItemBase weapon) {
        playerItemList = itemList;
        playerWeapon = weapon;
    }


    //プレイヤーのアイテムを渡す
    public List<ItemBase> GetPlayerItems() {  return playerItemList; }
    //プレイヤーの武器を渡す
    public ItemBase GetPlayerWeapon() { return playerWeapon; }

    /// <summary>
    /// シーン遷移時にアイテムを不使用状態にする
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // コピーを作ってから回す
        var tempList = new List<ItemBase>(_useList);

        for (int i = 0; i < tempList.Count; i++) {
            // プレイヤーが持っている物だったらスルー
            if (tempList[i].isPlayerPosses) continue;

            // 不使用状態にする
            UnuseItem(tempList[i].itemID);
        }

        _useList = tempList;
    }

    /// <summary>
    /// アイテムリストをなくす
    /// </summary>
    public void Reset() {
        //アイテムリストをなくす
        playerItemList = new List<ItemBase>(5); ;

        //possessItemListに空を詰める
        //初期化
        for (int i = 0, max = 5; i < max; i++) {
            playerItemList.Add(null);
        }

        //武器を初期化
        playerWeapon = null;
}

}