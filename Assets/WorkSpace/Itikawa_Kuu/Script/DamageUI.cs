using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    //public GameObject enemy;
    
    [SerializeField]
    private Canvas damageUI;

    // 表示秒数
    private float activeTime = 1.0f;
    // 秒数カウント
    private float timeCount;
    [SerializeField]
    private TextMeshProUGUI damageText;
    //　フェードアウトするスピード
    private float fadeOutSpeed = 5f;
    //　移動のスピード
    private float moveSpeed = 0.4f;
    float alpha = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //damageText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // プロト明けたら復活させる
        /*
        transform.position = DamageUIAction.enemy.transform.position;
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        Debug.Log(transform.position);
        //transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position + Vector3.up);

        timeCount += Time.deltaTime;
        if (timeCount > activeTime) {
            timeCount = 0;
            //gameObject.SetActive(false);
        }
        */

        // プロト版の仮実装
        transform.rotation = Camera.main.transform.rotation;
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        //damageText.color = Color.Lerp(damageText.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);
        //　少しづつ透明にしていく
        alpha -= fadeOutSpeed * Time.deltaTime;
        //　テキストのcolorを設定
        damageText.color = new Color(1f, 0f, 0f, alpha);

        if (damageText.color.a <= 0.1f) {
            Destroy(gameObject);
        }
    }

    public void ChangeDamageText(string changeText) {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(changeText);
        damageText.text = stringBuilder.ToString();
    }
}
