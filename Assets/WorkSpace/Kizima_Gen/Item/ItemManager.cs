/*
 * @file ItemManager.cs
 * @brief �A�C�e����v����
 * @author kijima
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour{
    
    public static ItemManager instance;
    //�g�p���̃A�C�e��
    [SerializeField]
    Transform _useRoot;
    //���g�p��Ԃ̃A�C�e��
    [SerializeField]
    Transform _unuseRoot;
    //�A�C�e���̘g
    [SerializeField]
    ItemBase originItem;

    //�g�p���̃A�C�e��
    List<ItemBase> _useList = new List<ItemBase>();
    //���g�p��Ԃ̃A�C�e��
    List<ItemBase> _unuseList = new List<ItemBase>();

    const int _ITEM_MAX = 256;

    [SerializeField] List<GameObject> items = new List<GameObject>();

    private void Start() {
        Initialize();
    }


    private void Initialize() {
        instance = this;
        for(int i = 0; i < _ITEM_MAX; i++) {
            Instantiate(originItem, _unuseRoot);
            _useList.Add(originItem);
        }
        DebugScript.ActionTrigger();
    }

    /// <summary>
    /// �A�C�e���̎g�p
    /// </summary>
    /// <param name="item"></param>
    public void UseItem() {
        ItemBase item = GetUsableItem();
        if (item != null) {
            //�����΂�擪�ɂ���g����A�C�e�����g�p����
            ItemBase instance = Instantiate(item); // �� ���������厖�I�I
            instance.gameObject.transform.SetParent(_useRoot);
        }
    }

    /// <summary>
    /// �A�C�e���𖢎g�p��Ԃɂ���
    /// </summary>
    /// <param name="item"></param>
    public void UnuseItem(int ID) {
        ItemBase item = _useList[ID];
        item.transform.SetParent(_unuseRoot);
    }

    /// <summary>
    /// �g�p�\�ȃA�C�e����Ԃ�
    /// </summary>
    /// <returns></returns>
    private ItemBase GetUsableItem() {
        for(int i = 0,max =_useList.Count; i< max; i++) {
            if (_useList[i] != null) 
                return _useList[i];
            
        }
        return null;
    }

}
