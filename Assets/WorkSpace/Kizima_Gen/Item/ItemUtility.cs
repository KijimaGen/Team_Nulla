/*
 * @file ItemUtility.cs
 * @brief ƒAƒCƒeƒ€•Ö—˜ŠÖ”
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
        //‚±‚±‚ÅY²‰ÁZ‚µ‚Ä–„‚Ü‚ç‚È‚¢‚æ‚¤‚É‚·‚é
        removePos.y += 1;

        ItemManager.instance.RemoveItem(ID, removePos);
    }
}
