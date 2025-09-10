/*
 * @file ItemBase.cs
 * @brief アイテム基底処理
 * @author kijima
 * @date 2025/9/10
 */
using UnityEngine;
using static UnityEditor.Progress;

public class ParkGetObject : MonoBehaviour{

    //プレイヤーに触れているかどうか
    private bool isPlayerInRange;
    /// <summary>
    /// プレイヤーと自身の当たり判定検知
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "Player") {
            isPlayerInRange = true;
            //UIの文字を変えて表示をつける
            UIManager.instance.ChangeVisibleinteractCanvas(true);
            UIManager.instance.ChangeInteractText("OpenPark");
        }
    }

    /// <summary>
    /// プレイヤーと部屋の離れ判定検知
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.tag == "Player") {
            isPlayerInRange = false;
            //UIの表示を切る
            UIManager.instance.ChangeVisibleinteractCanvas(false);
        }
    }
    private void OnEnable() {
        //プレイヤーイベントを購読
        PlayerOpenChester.OnInteract += TryOpenParkList;
    }

    private void OnDisable() {
        //イベント購読解除
        PlayerOpenChester.OnInteract -= TryOpenParkList;
    }

    /// <summary>
    /// 名前は完全なる嘘ですこれはアイテムを拾うためのスクリプト
    /// </summary>
    private void TryOpenParkList() {
        //近くにプレイヤーいなければ何もしない
        if (!isPlayerInRange) return;
        
        //UIの表示を切る
        UIManager.instance.ChangeVisibleinteractCanvas(false);
        AudioManager.instance.PlaySE(3);
        
    }
}
