/*
* @file HealItem.cs
* @brief アクセサリーアイテム
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CommonModule;




public class HealItem : ItemBase {
    //自身の追加体力
    private int HealValue;
    //自身のステータスを表示する奴の追加体力を表示する奴
    [SerializeField]
    private TextMeshProUGUI Status;


    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Initialize() {
        base.Initialize();

        HealValue = GetRandomFromRare(rarity);
        //バリューを10分の1にしないと回復量やばい
        HealValue /= 10;
        HealValue += 1;
        Status.text = "Heal + " + HealValue;


    }

    /// <summary>
    /// 回復力を渡す
    /// </summary>
    /// <returns></returns>
    public float GetHealValue() {
        return (int) HealValue;
    }

    public override bool isWeapon() {
        return false;
    }
}

