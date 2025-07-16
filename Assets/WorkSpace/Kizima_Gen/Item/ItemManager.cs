/*
 * @file ItemManager.cs
 * @brief �A�C�e����v����
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SystemObject{
    public static ItemManager instance;

    //�A�C�e�����Ăяo����̎Q��
    [SerializeField] Transform _useRoot;
    [SerializeField] Transform _unuseRoot;
    [SerializeField] ItemBase originItem;

    [SerializeField] List<GameObject> items;

    //�g�p�A�s�g�p���X�g
    List<ItemBase> _useList = new List<ItemBase>();
    List<ItemBase> _unuseList = new List<ItemBase>();

    //�A�C�e���̍ő吔
    const int _ITEM_MAX = 256;

    /// <summary>
    /// ������
    /// </summary>
    public override void Initialize() {
        instance = this;
        for (int i = 0; i < _ITEM_MAX; i++) {
            ItemBase item = Instantiate(originItem, _unuseRoot);
            item.Initialize();
            
            _unuseList.Add(item);
        }
    }

    /// <summary>
    /// �A�C�e�����g�����Ԃɂ���
    /// </summary>
    public void UseItem() {
        ItemBase item = GetUsableItem();
        if (item != null) {
            _unuseList.Remove(item);
            _useList.Add(item);

            
            item.transform.SetParent(_useRoot);
           
        }
        else {
            Debug.LogWarning("�g�p�\�ȃA�C�e��������܂���I");
        }
    }

    /// <summary>
    /// �A�C�e���𖢎g�p��Ԃɂ���
    /// </summary>
    /// <param name="ID"></param>
    public void UnuseItem(int ID) {
        if (_useList[0] ==  null) return;
        ItemBase item = _useList[ID];

        if (_useList.Contains(item)) {
            _useList.Remove(item);
            _unuseList.Add(item);

            item.transform.SetParent(_unuseRoot);
            
        }
    }

    /// <summary>
    /// �g�p�\�ȃA�C�e����Ԃ�
    /// </summary>
    /// <returns></returns>
    private ItemBase GetUsableItem() {
        if (_unuseList.Count > 0) {
            return _unuseList[0]; // �擪�̖��g�p�A�C�e����Ԃ�
        }
        return null;
    }

    public void 
}