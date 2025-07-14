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
   
    public static void UseItem() {
        ItemManager.instance.UseItem();
    }

    public static void UnuseItem(int ID) {
        ItemManager.instance.UnuseItem(ID);
    }
}
