/*
* @file ItemFrame.cs
* @brief アイテムスロット一個
* @author kijima
* @date 2025/7/23
*/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour{
    [SerializeField]
    private Image _itemIcon;



    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize() {
        _itemIcon.sprite = null;
    }

    /// <summary>
    /// 自身のアイコンの変更
    /// </summary>
    public void ChangeItemIcon() {
        _itemIcon.sprite = null;
    }

}
