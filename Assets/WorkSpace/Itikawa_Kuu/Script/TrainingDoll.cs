using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

using static ModeSwitch;

public class TrainingDoll : MonoBehaviour
{
    // 攻撃発生のクールタイム
    private float coolTime = 3;
    private float attackTime = 2;
    private float count = 0;
    // 人形の位置
    private Vector3 dollPos = Vector3.zero;
    // 攻撃のCollider
    [SerializeField]
    private Collider attackCollider;
    // 弾
    [SerializeField]
    private GameObject bullet;
    // 弾の発射間隔
    private float interval = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dollPos = transform.position;
        attackMode = true;
        // モード切り替え処理
        if (attackMode)
        {
            count += Time.deltaTime;
            //TrainingShoot();
            TrainingAttack();
            
            Debug.Log(count);
        } else {
            attackCollider.enabled = false;
        }
    }

    /// <summary>
    /// 攻撃モード処理
    /// </summary>
    private void TrainingAttack() {
        if (coolTime < count) {
            attackCollider.enabled = true;
            // 攻撃発生持続
            if (coolTime + attackTime < count) {
                count = 0;
            }
        } else {
            attackCollider.enabled = false;
        }
    }

    /// <summary>
    /// 射撃モード処理
    /// </summary>
    private void TrainingShoot() {
        // 一定間隔で撃つ
        if (count > interval) {
            Instantiate(bullet, new Vector3(dollPos.x + 1, dollPos.y, dollPos.z), Quaternion.identity);
            count = 0;
        }
    }
}
