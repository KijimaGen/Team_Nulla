/*
 * @file ItemBase.cs
 * @brief アイテム基底処理
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour{

    //落下すっぴーど
    protected const float fallSpeed = 0.001f;
    //地面にいるかどうか
    public bool isGround = false;





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

   

    private void OnTriggerEnter(Collider other) {
        isGround = true;
    }
    private void OnTriggerExit(Collider other) {
        isGround=false;
    }




}
