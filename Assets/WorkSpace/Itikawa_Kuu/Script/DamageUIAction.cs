using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIAction : MonoBehaviour
{
    // �I���W�i��UI
    public GameObject damageUI;
    // �N���[��UI
    public static GameObject cloneDamageUI;
    // Start is called before the first frame update
    void Start()
    {
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
        //cloneDamageUI.SetActive(true);
    }
}
