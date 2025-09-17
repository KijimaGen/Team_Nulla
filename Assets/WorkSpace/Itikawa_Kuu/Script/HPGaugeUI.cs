using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGaugeUI : MonoBehaviour
{
    // 表面のHPゲージ
    [SerializeField]
    private Image HPImage;
    // ダメージを受けた時の徐々に減るゲージ
    [SerializeField]
    private Image DamageImage;
    // 最大HP
    private float MaxHP = 10;
    // HP1当たりの幅
    private float HP1Width = 0;
    // 赤ゲージが動き出すまでの時間
    private float waitTime = 0.5f;
    // デバッグ用仮ダメージ
    private float damage = 0;

    Vector2 gauge;

    // Start is called before the first frame update
    void Start()
    {
        // ゲージの幅と高さ
        gauge = HPImage.rectTransform.sizeDelta;
        // ゲージの幅を最大HPで割る
        HP1Width = gauge.x / MaxHP;

        // 仮
        damage = HP1Width * 1;

    }

    // Update is called once per frame
    void Update()
    {
        HPImage.rectTransform.sizeDelta = gauge;
    }

    public void DamageGaugeDown() {
        DamageImage.rectTransform.sizeDelta = gauge;
    }

    //public void DamageProcess() {
    //    gauge.x -= damage;
    //    Invoke(nameof(DamageGaugeDown), waitTime);
    //}

    private void OnCollisionEnter(Collision collider) {
        if (collider.gameObject.tag == "Bullet") {
            gauge.x -= damage;
            Invoke(nameof(DamageGaugeDown), waitTime);
        }
    }
}
