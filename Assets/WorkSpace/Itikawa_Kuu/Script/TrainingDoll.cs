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
    // 攻撃のCollider
    [SerializeField]
    private Collider attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // モード切り替え処理
        if (attackMode)
        {
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
