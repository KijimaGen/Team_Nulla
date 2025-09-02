/*
 * @file ItemBase.cs
 * @brief アイテム基底処理
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour{

    //落下すっぴーど
    protected const float fallSpeed = 0.01f;
    //地面にいるかどうか
    public bool isGround = false;
    //自身のID
    public int itemID = -1;
    //プレイヤーが持っているかどうか
    public bool isPlayerPosses = false;
    //デバッグ用のplayer
    public GameObject player;
    
    //上下移動しながら回転するための者
    //上下の幅
    private float Amplitude = 0.5f;
    //速さ
    private float floatFrequency = 1.0f;
    //回転の速度
    private float rotationSpeed = 90f;
    //初期位置


    /// <summary>
    /// 初期化処理(基底クラスに任せる)
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// 落下処理
    /// </summary>
    public void Fall() {
        if (isGround) return;
        Vector3 fall = new Vector3 (0, fallSpeed, 0);
        this.transform.position -= fall;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Room") {
            isGround = true;

        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Room") {
            isGround = false;
        }
    }

    /// <summary>
    /// アイテムIDのセット
    /// </summary>
    /// <param name="ID"></param>
    public void SetItemID(int ID) {
        this.itemID = ID;
    }

    

    private void Update() {
        
    }
}
