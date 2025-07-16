/*
* @file ParkManager.cs
* @brief �p�[�N�̊Ǘ���
* @author kijima
* @date 2025/7/16
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkManager : SystemObject {

    //�p�[�N�̍ő吔
    private int _parkSelectMax = 3;
    
    //�p�[�N�Ǘ����X�g
    private List<Park> _parks = new List<Park>();

    [SerializeField]
    private Park _parkOrigin = null;

    [SerializeField]
    private GameObject _parkRoot = null;

    //�p�[�N��̎w���Ă����
    private int Index = 1;

    

    /// <summary>
    /// ������
    /// </summary>
    public override void Initialize() {
        for(int i = 0,max = _parkSelectMax; i< max; i++) {
            //�p�[�N��u���L�����o�X�Ƀp�[�N�̃A�C�e���𐶐�
            Park park = Instantiate(_parkOrigin, _parkRoot.transform);
            //������
            
            //���X�g�ɒǉ�
            _parks.Add(park);
        }
        _parkRoot.SetActive(false);
    }

    private void Update() {
        //�f�o�b�O�p�̊m�F
        if(Input.GetKeyDown(KeyCode.Z)) {
            //�������łɃp�[�N���J����Ă����ꍇ�I��ł���p�[�N���g�p
            if (_parkRoot.activeSelf) {
                _parks[Index].SelectPark();
                //�g�p���ďI��
                _parkRoot.SetActive(false);
                ExecuteAllPark(park => park.TearDown());
                return;
            }
            _parkRoot.SetActive(true);
            ChangeParkID();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            if (!_parkRoot.activeSelf) return;
            _parkRoot.SetActive(false);
            ExecuteAllPark(park => park.TearDown());
        }

        //�L�[�̓��͂ɂ���ăC���f�b�N�X��ύX
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Index++;
            if(Index >= _parks.Count) {
                Index = 0;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Index--;
            if (Index < 0) {
                Index = _parks.Count -1;
            }
            
        }
        //�S�A�E�g���C���̋@�\���~
        ExecuteAllPark(park => park.ChangeOutline(false));
        //�C���f�b�N�X�������Ă���҂���outline������
        _parks[Index].ChangeOutline(true);
    }

    /// <summary>
    /// �p�[�N�̍ő吔��ς���
    /// </summary>
    /// <param name="parkMax"></param>
    public void ChangeParkMax(int parkMax) {
        _parkSelectMax = parkMax;


    }

    /// <summary>
    /// �p�[�N�A�C�e���̍Đ���
    /// </summary>
    public void ChangeParkID() {
        for (int i = 0, max = _parks.Count; i < max; i++) {
            int randomMasterID = Random.Range(0, MasterdataManager.parkData[0].Count);
            //���Ԃ�Ȃ��Ȃ�܂Ń��[�v
            while (CheckSomeParkID(randomMasterID)) {
                randomMasterID = Random.Range(0, MasterdataManager.parkData[0].Count);
            }
            _parks[i].Initialize(randomMasterID);
        }
    }

    /// <summary>
    /// �n���ꂽ���������Ƀp�[�N�̃��X�g�ɂȂ������`�F�b�N
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private bool CheckSomeParkID(int ID) {
        for(int i = 0,max = _parks.Count;i < max;i++) {
            if(ID == _parks[i].GetID()) return true;
        }
        return false;
    }

    private void ExecuteAllPark(System.Action<Park> action) {
        if (action == null ) return;
        for (int i = 0, max = _parks.Count; i < max; i++) {
            if (_parks[i] == null) continue;
            action(_parks[i]);
        }
    }
   
}
