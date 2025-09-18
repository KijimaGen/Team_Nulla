/*
* @file EffectManager.cs
* @brief エフェクトの実体が必要なものとかをわたすよ～
* @author kijima
* @date 2025/9/16
*/

using System.Collections.Generic;
using UnityEngine;
using static GameConst;

public class EffectManager : SystemObject {

    //自身のインスタンス
    public static EffectManager instance;
    //レアリティごとのエフェクト
    [SerializeField]
    private List<GameObject> RarityEffectList;


    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Initialize() {
        instance = this;
        //シーン遷移しても壊れない
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// レアリティとトランスフォームを受け取り、トランスフォームにレアリティに対応したエフェクトを呼び出す
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="rarity"></param>
    public void InstantiateEffectFromRare(Transform transform,Rarity rarity) {
        if (RarityEffectList.Count < (int) rarity)
            return ;
        Instantiate( RarityEffectList[(int) rarity - 1],transform);
    } 


}
