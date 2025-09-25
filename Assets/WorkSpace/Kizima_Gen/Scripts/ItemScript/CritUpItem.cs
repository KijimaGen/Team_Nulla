/*
* @file CritUpItem.cs
* @brief アクセサリーアイテム
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CommonModule;




public class CritUpItem : ItemBase {
    //自身の追加体力
    private int CritUpValue;
    //自身のステータスを表示する奴の追加体力を表示する奴
    [SerializeField]
    private TextMeshProUGUI CritUpStuts;


    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Initialize() {
        base.Initialize();

        CritUpValue = GetRandomFromRare(rarity);
        CritUpStuts.text = "CritRate + " + CritUpValue;


    }

    /// <summary>
    /// 追加体力を渡す
    /// </summary>
    /// <returns></returns>
    public float GetCritUpValue() {
        return (int) CritUpValue;
    }

    public override bool isWeapon() {
        return false;
    }
}

