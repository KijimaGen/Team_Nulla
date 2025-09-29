/*
 * @file CommonModule.cs
 * @brief 便利な機能まとめ(てか今まで作ってなかったのか💦)
 * @author kijima
 * @date 2025/9/16
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConst;

public static class CommonModule { 
    //レアリティを受け取って文字列を返す
    public static string RareToString(Rarity rarity) {
        switch (rarity) {
            case Rarity.Common:
                return "Common"; 
                
            case Rarity.Uncommon:
                return "Uncommon";
            case Rarity.Rare:
                return "Rare";
            case Rarity.Epic:
                return "Epic";
            case Rarity.Legendary:
                return "Legendary";
        }
        return null;
    }

    public static Rarity GetRandomRarity() {
        // 各レアリティの重みを設定（数値が大きいほど出やすい）
        Dictionary<Rarity, int> weights = new Dictionary<Rarity, int>() {
        { Rarity.Common,    50 }, // 50%
        { Rarity.Uncommon,  25 }, // 25%
        { Rarity.Rare,      15 }, // 15%
        { Rarity.Epic,       7 }, // 7%
        { Rarity.Legendary,  3 }, // 3%
    };

        // 総和を出す
        int totalWeight = 0;
        foreach (var w in weights.Values) {
            totalWeight += w;
        }

        // 0〜totalWeight-1 の乱数を生成
        int rand = Random.Range(0, totalWeight);

        // 重みに応じて決定
        foreach (var kvp in weights) {
            rand -= kvp.Value;
            if (rand < 0) {
                return kvp.Key;
            }
        }

        return Rarity.Common; // フォールバック（ここには来ない想定）
    }

    //レアリティを受け取ってそれに合わせたランダムな値を返す
    public static int GetRandomFromRare(Rarity rarity) {
        switch (rarity) {
            case Rarity.Common:
                return Random.Range(5, 20);
            case Rarity.Uncommon:
                return Random.Range(21, 40);
            case Rarity.Rare:
                return Random.Range(41, 60);
            case Rarity.Epic:
                return Random.Range(61, 80);
            case Rarity.Legendary:
                return Random.Range(81, 100);
        }
        return 0;
    }
    //レアリティを受け取って色を返す


    /// <summary>
	/// リストが空か判定
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static bool IsEmpty<T>(List<T> list) {
        // 短絡評価なので大丈夫
        return list == null || list.Count <= 0;
    }


    /// <summary>
    /// リストに対して有効なインデクスか判定
    /// </summary>
    /// <returns></returns>
    public static bool IsEnableIndex<T>(List<T> list, int index) {
        if (IsEmpty(list)) return false;

        return index >= 0 && list.Count > index;
    }


    /// <summary>
    /// リストがいっぱいか確認
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsFullList<T>(List<T> list) {

        for(int i = 0,max = list.Count;i < max; i++) {
            if (list[i] != null) continue;

            return false;
        }
        return true;
    }

    /// <summary>
    /// クリティカル率を元に計算
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="criticalRate"></param>
    /// <param name="criticalDamageRate"></param>
    /// <returns></returns>
    public static int CalcClit(int damage,int criticalRate,int criticalDamageRate) {
        //確率の種を生成
        int rand = Random.Range(0, 101);
        //クリティカル判定
        if (rand < criticalRate) {
            AudioManager.instance.PlaySE(10);

            float returnDamage = damage * criticalDamageRate / 100;

            return (int)returnDamage;
        }

        //クリティカルの戦いに負けたのでそのまま帰ります
        return damage;
    }



}
