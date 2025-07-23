using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class TrainingDoll : MonoBehaviour
{
    // モード切替のフラグ
    private bool attackMode = false;
    // 攻撃発生のクールタイム
    private float coolTime = 5;
    private float attackTime = 2;
    private float count = 0;
    // 攻撃のCollider
    [SerializeField]
    private Collider attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        attackMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        // モード切り替え処理
        if (attackMode)
        {
            TrainingAttack();
            Debug.Log(count);
        }
        
    }

    /// <summary>
    /// 攻撃モード処理
    /// </summary>
    private void TrainingAttack() {
        count += Time.deltaTime;
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
}
