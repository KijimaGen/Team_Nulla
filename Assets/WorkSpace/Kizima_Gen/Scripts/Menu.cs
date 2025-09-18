/**
 * @file Menu.cs
 * @brief メニューに着くスクリプト
 * @author kijima
 * @date 2025/9/18
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour{
    private List<Sprite> ItemIcons = new List<Sprite>(5);


    void Start(){
       
    }

    void Update(){
        
    }


    /// <summary>
    /// メニューを開く
    /// </summary>
    void OpenMenu() {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerCharacter>().SendItemList();

        //アイテムリストを初期化して開く
        List<ItemBase> itemList = ItemManager.instance.GetPlayerItems();
        for (int i = 0; i < ItemIcons.Count; i++) {
            ItemIcons[i] = itemList[i].myIcon;
        }
    }
}
