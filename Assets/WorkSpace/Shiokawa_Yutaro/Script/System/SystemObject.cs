/*
* @file SystemObject.cs
* @brief �قڂ��ׂẴ}�l�[�W���[�̊��
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SystemObject : MonoBehaviour{

    /// <summary>
    /// ����������
    /// </summary>
    public abstract void Initialize();
    
}
