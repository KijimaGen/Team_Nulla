using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIAction : MonoBehaviour
{
    // �I���W�i��UI
    public GameObject damageUI;
    // �N���[��UI
    public static GameObject cloneDamageUI;
    // �������g
    public static GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject;

        cloneDamageUI = Instantiate(damageUI, transform.position, Quaternion.identity);
        // ��A�N�e�B�u�ɂ��Ă���
        //cloneDamageUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(damageUI.activeSelf);
    }

    // �U��������������A�N�e�B�u�ɂ���
    private void OnCollisionEnter(Collision collision) {
        cloneDamageUI.SetActive(true);
    }
}
