/*
* @file ItemAccessory.cs
* @brief アクセサリーアイテム
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAccessory : ItemBase {


    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Initialize() {
        
    }

    public override bool isWeapon() {
        return false;
    }
}
