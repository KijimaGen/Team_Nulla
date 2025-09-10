/*
 * @file UIManager.cs
 * @brief 音の処理まとめ
 * @author kijima
 * @date 2025/9/10
 */
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UIManager : SystemObject {
    //宝箱に近づいた時に出てくるキャンバス
    [SerializeField]
    private Canvas interactCanvas;
    //の中のテキスト
    [SerializeField]
    private TextMeshProUGUI interactText;
    //の中に固定で置いておくテキスト
    private const string INTERACT_FIXED_TEXT = "A:";

    //自身のインスタンス
    public static UIManager instance;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public override void Initialize() {
        instance = this;
        interactCanvas.enabled = false;
    }

    /// <summary>
    /// UIを見せるキャンバスを表示するかどうか変える
    /// </summary>
    /// <param name="visible"></param>
    public void ChangeVisibleinteractCanvas(bool visible) {
        interactCanvas.enabled = visible;
    }

    /// <summary>
    /// テキストを変更
    /// </summary>
    /// <param name="changetext"></param>
    public void ChangeInteractText(string changetext) {
        StringBuilder sb = new StringBuilder();
        sb.Append(INTERACT_FIXED_TEXT);
        sb.Append(changetext);
        interactText.SetText(sb);
    }

}
