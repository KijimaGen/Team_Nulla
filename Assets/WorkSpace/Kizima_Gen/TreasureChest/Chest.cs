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
    //自身から排出するアイテム
    [SerializeField]
    private GameObject itemPrefab;

    //アイテムを生成するときに呼び出すエフェクト
    [SerializeField]
    private GameObject smokePrefab;
    //自身が破壊されるときのエフェクト
    [SerializeField]
    private GameObject hitEffectPrefab;

    private void Update() {
        if (itemPrefab != null) { 
            if(Input.GetKeyDown(KeyCode.E)) {
                Instantiate(itemPrefab,this.transform.position,Quaternion.identity);
                Instantiate(smokePrefab,this.transform.position,Quaternion.identity);
                Instantiate(hitEffectPrefab, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

}
