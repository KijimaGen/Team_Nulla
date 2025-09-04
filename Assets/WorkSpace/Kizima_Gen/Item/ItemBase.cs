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
    

    //プレイヤーに触れているかどうか
    private bool isPlayerInRange;

    //上下移動しながら回転するための者
    
    //回転の速度
    private float rotationSpeed = 90f;
    

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
    /// プレイヤーと部屋の当たり判定検知
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Room") {
            isGround = true;
            
        }

        if(other.gameObject.tag == "Player") {
            isPlayerInRange = true;
        }
    }

    /// <summary>
    /// プレイヤーと部屋の離れ判定検知
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Room") {
            isGround = false;
        }

        if (other.gameObject.tag == "Player") {
            isPlayerInRange = false;
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
        
        

        //プレイヤーに触れていたら自身を未使用状態にする

        if (isPlayerPosses)
            transform.localPosition = Vector3.zero;
        else
            Fall();
        
            //Y軸回転
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            
        
    }


    private void OnEnable() {
        //プレイヤーイベントを購読
        PlayerOpenChester.OnInteract += TryOpenChest;
    }

    private void OnDisable() {
        //イベント購読解除
        PlayerOpenChester.OnInteract -= TryOpenChest;
    }

    /// <summary>
    /// 名前は完全なる嘘ですこれはアイテムを拾うためのスクリプト
    /// </summary>
    private void TryOpenChest() {
        //近くにプレイヤーいなければ何もしない
        if (!isPlayerInRange) return;
        if (isPlayerPosses) return;

        DebugScript.instance.PlaySE();
        ItemUtility.GetItem(itemID);
    }
}
