using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    // プール対象
    [SerializeField] private GameObject damageUI;
    // 初期プール数
    private int poolSize = 10;
    private List<GameObject> pool;

    // プールを生成
    private void Awake() {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++) {
            GameObject gameObjecrt = Instantiate(damageUI);
            gameObjecrt.SetActive(false);
            pool.Add(gameObjecrt);
        }

    }
}
