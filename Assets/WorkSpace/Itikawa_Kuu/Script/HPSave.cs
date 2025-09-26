using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSave : MonoBehaviour
{
    // HPの保存場所
    public static float saveHP = 0;

    // 自身のインスタンス
    public static HPSave instance;

    /// <summary>
    /// 2回目以降は生成しない
    /// </summary>
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        saveHP = HPGaugeUI.restHP;
    }
}
