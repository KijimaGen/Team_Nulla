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

    //レアリティをランダムで抽選して返す
    public static Rarity GetRandomRarity() {
        // enum の全要素を配列に変換
        Rarity[] values = (Rarity[]) System.Enum.GetValues(typeof(Rarity));

        // Unity の Random.Range でランダム抽選
        int index = Random.Range(0, values.Length);
        return values[index];
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
}
