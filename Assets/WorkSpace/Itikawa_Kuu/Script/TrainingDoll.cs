using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // モード切替のフラグ
    private bool attackMode = false;
    // 攻撃発生のクールタイム
    private float attackTime = 5;
    private float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        count += Time.deltaTime;
        if (attackMode)
        {
            if (int i = 0; attackTime > count; int++) {
                count = 0;
                // 攻撃の処理
            }

        }
        */
    }
}
