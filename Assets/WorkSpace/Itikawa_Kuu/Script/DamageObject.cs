using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    // �v�[���Ώ�
    [SerializeField] private GameObject damageUI;
    // �����v�[����
    private int poolSize = 10;
    private List<GameObject> pool;

    // �v�[���𐶐�
    private void Awake() {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++) {
            GameObject gameObjecrt = Instantiate(damageUI);
            gameObjecrt.SetActive(false);
            pool.Add(gameObjecrt);
        }

    }
}
