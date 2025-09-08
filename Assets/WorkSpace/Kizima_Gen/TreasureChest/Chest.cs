/*
* @file Chest.cs
* @brief タカラバコ関連
* @author kijima
* @date 2025/9/2
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour{
    //アイテムを生成するときに呼び出すエフェクト
    [SerializeField]
    private GameObject smokePrefab;
    //自身が破壊されるときのエフェクト
    [SerializeField]
    private GameObject hitEffectPrefab;
    //プレイヤーに触れているかどうか
    private bool isPlayerInRange;

    //初期回転度
    private Vector3 natureRotation = new Vector3(-90,90,0);
    //初期ポジション
    private Vector3 naturePosition = new Vector3(0,1,0);

    /// <summary>
    /// プレイヤーに当たったらswitchを入れる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            isPlayerInRange = true;
            
        }
    }

    /// <summary>
    /// 出たタイミングでswitchを切る
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            isPlayerInRange = false;
        }
    }

    private void OnEnable() {
        //プレイヤーイベントを購読
        PlayerOpenChester.OnInteract += TryOpenChest;
    }

    private void OnDisable() {
        //イベント購読解除
        PlayerOpenChester.OnInteract -= TryOpenChest;
    }

    private void TryOpenChest() {
        //近くにプレイヤーいなければ何もしない
        if (!isPlayerInRange) return;

        Debug.Log("宝箱を開けた！");

        DebugScript.instance.PlaySound();
        ItemUtility.UseItem(this.transform.position);
        Instantiate(smokePrefab, this.transform.position, Quaternion.identity);
        Instantiate(hitEffectPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void Update() {
        if(transform.position.y < -0.5) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        //回転度を初期化
        transform.Rotate(natureRotation);
        //ポジションも初期化
        naturePosition = new Vector3(transform.position.x,naturePosition.y,transform.position.z);
        transform.position = naturePosition;
    }
}
