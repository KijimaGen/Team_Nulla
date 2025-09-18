using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPGaugeUI : MonoBehaviour
{
    // 表面のHPゲージ
    [SerializeField]
    private Image HPImage;
    // ダメージを受けた時の徐々に減るゲージ
    [SerializeField]
    private Image DamageImage;
    // 最大HP
    private float MaxHP = 100;
    // HP1当たりの幅
    private float HP1Width = 0;
    // 赤ゲージが動き出すまでの時間
    //private float waitTime = 0.5f;
    // 総合ダメージ
    private float damage = 0;
    // 弾の威力
    private float bulletDamage = 1;
    // 近接の威力
    private float punchDamage = 1;

    // HP
    public static float HP = 100;

    Vector2 gauge;

    // Start is called before the first frame update
    void Start()
    {
        // ゲージの幅と高さ
        gauge = HPImage.rectTransform.sizeDelta;
        // ゲージの幅を最大HPで割る
        HP1Width = gauge.x / MaxHP;
        //bulletDamage = Enemy_LongRange.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        HPImage.rectTransform.sizeDelta = gauge;

        if (gauge.x <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    //public void DamageGaugeDown() {
    //    DamageImage.rectTransform.sizeDelta = gauge;
    //}

    //public void DamageProcess() {
    //    gauge.x -= damage;
    //    Invoke(nameof(DamageGaugeDown), waitTime);
    //}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            // 弾のダメージ
            damage = HP1Width * bulletDamage;
            gauge.x -= damage;
            //Invoke(nameof(DamageGaugeDown), waitTime);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Bullet") {
            // 弾のダメージ
            damage = HP1Width * bulletDamage;
            gauge.x -= damage;
        }
        if (collider.gameObject.tag == "GoingAttack") {
            // 近接のダメージ
            damage = HP1Width * punchDamage;
            gauge.x -= damage;
        }
    }
}
