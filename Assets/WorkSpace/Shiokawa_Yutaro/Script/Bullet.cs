using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1f; // 弾の速度
    private Rigidbody rb;
    [SerializeField] private ParticleSystem _effect;
    Renderer render;
    [SerializeField]
    private GameObject BulletEffect;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 20, ForceMode.Impulse);
        render = GetComponent<Renderer>();
    }
    bool counterHit;
    float hitDistance = 0.1f;  // 近ければ当たり判定

     float horizontalAngleRange = 70f; // 左右ランダム回転の角度
     float horizontalSpeed = 2f;      // 水平方向スピード
    
     float downwardForce = 3f;        // 下方向に叩きつける強さ
    
     float maxTilt = 20f;              // X,Z軸回転の最大値

    void Update()
    {
        Destroy(gameObject, 5);

        if (this == null) return;
        // プレイヤーをレイヤーで検索
        Collider[] players = Physics.OverlapSphere(transform.position, hitDistance, LayerMask.GetMask("Player"));
        foreach (var playerCol in players)
        {
            if (counterHit) return;
            // プレイヤーに当たった
            PlayerAction playerAction = playerCol.GetComponent<PlayerAction>();
                
          
            if (playerCol is SphereCollider && playerAction.isDashing)
            {
                counterHit = true;
                Instantiate(_effect, transform.position, transform.rotation);
                rb.constraints = RigidbodyConstraints.None;

                // 1. プレイヤーからの方向（水平のみ）
                Vector3 hitDir = (transform.position - playerCol.transform.position);
                hitDir.y = 0f;
                hitDir.Normalize();

                // 2. 左右ランダム回転
                float angle = Random.Range(-horizontalAngleRange, horizontalAngleRange);
                hitDir = Quaternion.AngleAxis(angle, Vector3.up) * hitDir;

                // 3. 水平方向の速度設定
                Vector3 horizontalVel = hitDir * horizontalSpeed;

                // 4. 下方向に強制的に加速
                Vector3 newVelocity = horizontalVel + Vector3.down * downwardForce;

                // 5. Rigidbody に反映
                rb.velocity = newVelocity;

                // 6. ランダム回転を付与
                float randomTiltX = Random.Range(-maxTilt, maxTilt);
                float randomTiltZ = Random.Range(-maxTilt, maxTilt);
                rb.AddTorque(new Vector3(randomTiltX, 0f, randomTiltZ), ForceMode.VelocityChange);

                //効果音を再生
                AudioManager.instance.PlaySE(1);

                render.material.color = Color.red;
            }
            else if (playerCol is not SphereCollider) {
                //エフェクト出して消えるよ～～～～～～～～～～～～ん
                Instantiate(BulletEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                //playerCol.GetComponent<HPGaugeUI>().DamageProcess();
                //playerCol.GetComponent<HPGaugeUI>().DamageGaugeDown();
            }
            else {
                //エフェクト出して消えるのにぇん！
                Instantiate(BulletEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

    }

    //private void OnCollisionEnter(Collision collision) {
    //    if(collision.gameObject.tag != "Player") {
    //        //エフェクト出して消えるペコ！
    //        Instantiate(BulletEffect, transform.position, transform.rotation);
    //        Destroy(gameObject);
    //    }
    //}
}
