/*
* @file Park.cs
* @brief パークの管理者
* @author kijima
* @date 2025/7/16
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ParkMasterUtility;
using static ParkIconSender;

public class Park : MonoBehaviour{
    //自身への参照
    [SerializeField]
    private Image _iconRoot = null;
    [SerializeField]
    private TextMeshProUGUI _nameText = null;
    [SerializeField]
    private TextMeshProUGUI _descriptionText = null;
    //自身のマスターID
    private int ID = -1;
    //自身のパークマスター
    Entity_Park.Param parkMaster = null;    

    public void Initialize(int ID) {
        this.ID = ID;

        //パークのマスターからの代入
        parkMaster = GetParkMaster(ID);
        _iconRoot.sprite = ParkIconSender.instatnce.GetParkIcon(parkMaster.IconID);
        _nameText.text = parkMaster.Name;
        _descriptionText.text = parkMaster.Text;
    }

    public int GetID() {
        return this.ID;
    }

    /// <summary>
    /// ここでIDを綺麗にしておくよ
    /// </summary>
    public void TearDown() {
        ID = -1;
    }
}
