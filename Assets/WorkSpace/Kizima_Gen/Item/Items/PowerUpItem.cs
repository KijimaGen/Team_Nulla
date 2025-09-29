/*
* @file PowerUpItem.cs
* @brief アクセサリーアイテム
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CommonModule;




public class PowerUpItem : ItemBase {
    //自身の攻撃力
    private int AttackValue;
    //自身のステータスを表示する奴の攻撃力を表示する奴
    [SerializeField]
    private TextMeshProUGUI AttackText;
    

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Initialize() {
        base.Initialize();
        
        AttackValue = GetRandomFromRare(rarity);
        //攻撃力下方修正の跡
        AttackValue = (int) (AttackValue * 0.25f);
        AttackText.text = "Attack + " + AttackValue;


    }

    /// <summary>
    /// 攻撃力を渡す
    /// </summary>
    /// <returns></returns>
    public float GetAttackValue() {
        return (int) AttackValue;
    }
    public override bool isWeapon() {
        return false;
    }
}

