/**
 * @file Menu.cs
 * @brief メニューに付くスクリプト
 * @author kijima
 * @date 2025/9/18
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour{
    //アイテムアイコンのデータ
    private List<Sprite> ItemIcons = new List<Sprite>(5);

    //実際に目に見えるアイコン
    [SerializeField]
    private List<Image> itemIcon = new List<Image>(5);

    //メニューの親
    [SerializeField]
    private GameObject menuParent;

    void Start(){
       
    }

    void Update(){
        
    }


    /// <summary>
    /// メニューを開く
    /// </summary>
    public void OpenMenu(InputAction.CallbackContext context) {
        
            //プレイヤーを探す
            GameObject player = GameObject.FindWithTag("Player");
            //アイテムリストをキャッシュして受け取る
            player.GetComponent<PlayerCharacter>().SendItemList();

            //アイテムリストを初期化して開く
            List<ItemBase> itemList = ItemManager.instance.GetPlayerItems();
            for (int i = 0; i < ItemIcons.Count; i++) {
                //受け取ったアイコンの情報をもらう
                ItemIcons[i] = itemList[i].myIcon;
                //情報を反映
                itemIcon[i].sprite = ItemIcons[i];
            }
            //メニュー画面を開く
            menuParent.SetActive(true);
        
    }

    /// <summary>
    /// メニュー画面を閉じる
    /// </summary>
    /// <param name="context"></param>
    public void CloseMenu(InputAction.CallbackContext context) {
       
            menuParent.SetActive(false);
        
    }


}
