using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI text;
    public static float speed = 1;
    public static float alpha = 0;
    public static bool repetitionFlag;
    // Start is called before the first frame update
    void Start() {
        repetitionFlag = true;
    }

    // Update is called once per frame
    void Update() {
        //text = gameObject.GetComponent<TextMeshProUGUI>();
        if (repetitionFlag) {
            alpha += Time.deltaTime * speed;
            if (alpha > 1) {
                repetitionFlag = false;
            }
        }
        else if (!repetitionFlag) {
            alpha -= Time.deltaTime * speed;
            if (alpha < 0) {
                repetitionFlag = true;
            }
        }
        text.color = new Color(255, 255, 255, alpha);
    }
}