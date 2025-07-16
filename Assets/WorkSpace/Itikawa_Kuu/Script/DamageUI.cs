using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    //public GameObject enemy;
    [SerializeField]
    private TextMeshProUGUI damage;
    [SerializeField]
    private Canvas damageUI;

    public Transform target;

    // 表示秒数
    private float activeTime = 1.0f;
    // 秒数カウント
    private float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = DamageUIAction.enemy.transform.position;
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        Debug.Log(transform.position);
        //transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position + Vector3.up);

        timeCount += Time.deltaTime;
        if (timeCount > activeTime) {
            timeCount = 0;
            gameObject.SetActive(false);
        }
    }
}
