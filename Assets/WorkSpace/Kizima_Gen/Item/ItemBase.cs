/*
 * @file ItemBase.cs
 * @brief �A�C�e����ꏈ��
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour{

    //���������ҁ[��
    protected const float fallSpeed = 0.001f;
    //�n�ʂɂ��邩�ǂ���
    public bool isGround = false;





    /// <summary>
    /// ����������(���N���X�ɔC����)
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// ��������
    /// </summary>
    public void Fall() {
        if (isGround) return;
        Vector3 fall = new Vector3 (0, fallSpeed, 0);
        this.transform.position -= fall;
    }

   

    private void OnTriggerEnter(Collider other) {
        isGround = true;
    }
    private void OnTriggerExit(Collider other) {
        isGround=false;
    }




}
