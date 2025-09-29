/*
* @file CritDamageUpItem.cs
* @brief アクセサリーアイテム
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CommonModule;




public class CritDamageUpItem : ItemBase {
    //自身の追加クリティカルダメージ
    private int CritDamageUpValue;
    //自身のステータスを表示する奴の追加体力を表示する奴
    [SerializeField]
    private TextMeshProUGUI CritDamageUpStuts;


    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Initialize() {
        base.Initialize();

        CritDamageUpValue = GetRandomFromRare(rarity);
        CritDamageUpStuts.text = "CritDamage + " + CritDamageUpValue;


    }

    /// <summary>
    /// 追加クリティカルダメージ倍率を渡す
    /// </summary>
    /// <returns></returns>
    public float GetCritDamageUpValue() {
        return (int) CritDamageUpValue;
    }

    public override bool isWeapon() {
        return false;
    }
}

