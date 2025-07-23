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
        /*
        enemy = gameObject;

        cloneDamageUI = Instantiate(damageUI, transform.position, Quaternion.identity);
        // 非アクティブにしておく
        //cloneDamageUI.SetActive(false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(damageUI.activeSelf);
    }
    
    // 攻撃が当たったらアクティブにする
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            //cloneDamageUI.SetActive(true);
            Damage(collider);
        }
    }
    
    // プロトの仮実装
    public void Damage(Collider collider) {
        //　DamageUIを中心からカメラの方向に少し寄せた位置にインスタンス化
        var obj = Instantiate(damageUI, collider.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
    }
}
