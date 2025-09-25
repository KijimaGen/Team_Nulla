using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core.Easing;

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
    private float waitTime = 0.5f;
    // 総合ダメージ
   //private float damage = 0;
    // 弾の威力
    private float bulletDamage = 5;
    // 近接の威力
    private float punchDamage = 1;

    Vector2 gauge;

    PlayerAction playerAction;
    PlayerCharacter player;

    // Start is called before the first frame update
    void Start()
    {
        playerAction = GetComponent<PlayerAction>();
        player = GetComponent<PlayerCharacter>();
        // ゲージの幅と高さ
        gauge = HPImage.rectTransform.sizeDelta;
        // ゲージの幅を最大HPで割る
        HP1Width = gauge.x / player.maxHP;

        //bulletDamage = Enemy_LongRange.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.GetHP());
        HPImage.rectTransform.sizeDelta = gauge;
        if(Time.timeScale <= 1 && !playerAction.isJustAvoiding)
        {
            //Time.timeScale += 0.02f;
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
            SetDamage(bulletDamage);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Bullet")
        {
            // 弾のダメージ
            SetDamage(bulletDamage);
        }
        if (collider.gameObject.tag == "Enemy" && collider is BoxCollider)
        {
            if (playerAction.isJustAvoiding)
            {
                //Time.timeScale = 0.2f;
                return;
            }
            if (playerAction.isAvoiding) return;

            //if (collider.gameObject.tag == "Bullet") {
            //    // 弾のダメージ
            //    damage = HP1Width * bulletDamage;
            //    gauge.x -= damage;
            //}


            EnemyCharacter enemy = collider.GetComponent<EnemyCharacter>();
            // 近接のダメージ            
            SetDamage(enemy.rawAttack);
        }
    }

    float duration = 0.2f;
    float HcurrentRate = 1.0f;
    private void UpdateFillAmount(Image frontImage, ref float currentRate, float targetRate, float duration, Image burnImage = null)
    {
        // 0〜1の範囲に制限
        targetRate = Mathf.Clamp01(targetRate);

        // DOTweenでFillAmountのアニメーション
        frontImage.DOFillAmount(targetRate, duration).OnComplete(() =>
        {
            if (burnImage == null) return;
            burnImage.DOFillAmount(targetRate, duration).SetDelay(0.3f);
        });

        // currentRateの更新
        currentRate = targetRate;
    }

    public void SetDamage(float _damage)
    {
        float damage = Mathf.Max(_damage - player.rawDefense, 1);
        player.SetHP(player.GetHP() - damage);
        float targetRate = HcurrentRate - _damage / player.maxHP;
        UpdateFillAmount(HPImage, ref HcurrentRate, targetRate, duration, DamageImage);
    }
}
