/*
* @file Park.cs
* @brief �p�[�N�̊Ǘ���
* @author kijima
* @date 2025/7/16
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ParkMasterUtility;

public class Park : MonoBehaviour{
    //���g�ւ̎Q��
    [SerializeField]
    private Image _iconRoot = null;
    [SerializeField]
    private TextMeshProUGUI _nameText = null;
    [SerializeField]
    private TextMeshProUGUI _descriptionText = null;
    [SerializeField]
    private Outline _outline = null;
    //���g�̃}�X�^�[ID
    private int ID = -1;
    //���g�̃p�[�N�}�X�^�[
    Entity_Park.Param parkMaster = null;    
    

    public void Initialize(int ID) {
        this.ID = ID;

        //�p�[�N�̃}�X�^�[����̑��
        parkMaster = GetParkMaster(ID);
        _iconRoot.sprite = ParkIconSender.instatnce.GetParkIcon(parkMaster.IconID);
        _nameText.text = parkMaster.Name;
        _descriptionText.text = parkMaster.Text;
    }

    public int GetID() {
        return this.ID;
    }

    /// <summary>
    /// ������ID���Y��ɂ��Ă�����
    /// </summary>
    public void TearDown() {
        ID = -1;
    }

    public void ChangeOutline(bool isEnabled) {
        _outline.enabled = isEnabled;
    }

    public void SelectPark() {
        Debug.Log(_nameText.text+ "��I�����܂���");
    }

}
