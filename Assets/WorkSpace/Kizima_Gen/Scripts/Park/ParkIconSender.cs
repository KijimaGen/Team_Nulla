/*
* @file ParkIconSender.cs
* @brief �p�[�N�̃A�C�R���������Ă�����
* @author kijima
* @date 2025/7/16
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkIconSender:MonoBehaviour{

    [SerializeField]
    private List<Sprite> _parkIcons = null;

    //���g�̃C���X�^���X(�ǂ��ɂ�����ł�����񂩂ȁc)
    public static ParkIconSender instatnce;

    //������
    void Awake () { instatnce = this; }

    /// <summary>
    /// ID���󂯎���Ă���ɍ������A�C�R���������n��
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Sprite GetParkIcon(int ID) {
        return _parkIcons[ID];
    }
}
