/*
* @file ParkIconSender.cs
* @brief パークのアイコンを持っているよん
* @author kijima
* @date 2025/7/16
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkIconSender:MonoBehaviour{

    [SerializeField]
    private List<Sprite> _parkIcons = null;

    //自身のインスタンス(どうにか回避できんもんかな…)
    public static ParkIconSender instatnce;

    //初期化
    void Awake () { instatnce = this; }

    /// <summary>
    /// IDを受け取ってそれに合ったアイコンを引き渡す
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Sprite GetParkIcon(int ID) {
        return _parkIcons[ID];
    }
}
