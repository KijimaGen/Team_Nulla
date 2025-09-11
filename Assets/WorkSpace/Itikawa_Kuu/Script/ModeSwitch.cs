using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitch : MonoBehaviour
{
    // 案山子のモード切替フラグ
    public static bool attackMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (attackMode) {
            attackMode = false;

        } else if (!attackMode) {
            attackMode = true;
        }
    }
    /*
    // ヒットエフェクト
    [SerializeField]
    private ParticleSystem hitEffect;
    private void HitEffect(Collider collider) {
        ParticleSystem hitEffectClone = Instantiate(hitEffect, collider.transform.position, Quaternion.identity);
        Destroy(hitEffectClone.gameObject, 2);
    }
    */
}
