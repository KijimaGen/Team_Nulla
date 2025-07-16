using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIAction : MonoBehaviour
{
    // オリジナルUI
    public GameObject damageUI;
    // クローンUI
    public static GameObject cloneDamageUI;
    // 自分自身
    public static GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject;

        cloneDamageUI = Instantiate(damageUI, transform.position, Quaternion.identity);
        // 非アクティブにしておく
        //cloneDamageUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(damageUI.activeSelf);
    }

    // 攻撃が当たったらアクティブにする
    private void OnCollisionEnter(Collision collision) {
        cloneDamageUI.SetActive(true);
    }
}
