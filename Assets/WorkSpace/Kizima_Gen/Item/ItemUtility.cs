/*
 * @file ItemUtility.cs
 * @brief アイテム便利関数
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtility{
   
    public static void UseItem(Vector3 spawnPos) {
        ItemManager.instance.UseItem(spawnPos);
    }

    public static void UnuseItem(int ID) {
        ItemManager.instance.UnuseItem(ID);
    }

    public static void GetItem(int ID) {
        ItemManager.instance.GetItem(ID);
    }

    public static void RemoveItem(int ID, Vector3 removePos) {
        ItemManager.instance.RemoveItem(ID, removePos);
    }

    //プレイヤーのアイテムを渡す
    public static List<ItemBase> GetPlayerItems() { return ItemManager.instance.GetPlayerItems(); }
    //プレイヤーの武器を渡す
    public static ItemBase GetPlayerWeapon() { return ItemManager.instance.GetPlayerWeapon(); }
}
