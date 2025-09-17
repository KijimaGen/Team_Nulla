/*
 * @file PlayerPossesItemManager.cs
 * @brief プレイヤーがなんのアイテムを持っているかを管理する
 * @author kijima
 * @date 2025/9/16
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPossesItemManager : SystemObject {

    //プレイヤーが持てるアイテムのマックス
    int PlayerAccessoryMax = 5;
    //プレイヤーが持つアイテムのリスト
    //private List<PowerUpItem> playerAccessories = new List<PowerUpItem>();
    //持ってるアイテムの加算したいステータス一覧
    float HP;
    float Attack;




    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public override void Initialize() {

    }



}
