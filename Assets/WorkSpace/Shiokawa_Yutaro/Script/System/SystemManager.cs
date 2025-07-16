/*
* @file SystemManager.cs
* @brief �}�l�[�W���[�Ǘ��}�l�[�W���[(��̂������G���g���[�|�C���g)
* @author kijima
* @date 2025/7/16
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemManager : SystemObject {
    public List<SystemObject> systemObjectList = null;

    private void Start() {
        Initialize();
    }

    public override void Initialize() {
        // �S�V�X�e���I�u�W�F�N�g�̐����A������
        for (int i = 0, max = systemObjectList.Count; i < max; i++) {
            SystemObject origin = systemObjectList[i];
            if (origin == null) continue;
            // �V�X�e���I�u�W�F�N�g����
            SystemObject createObject = Instantiate(origin, transform);
            // ������
            createObject.Initialize();
        }
        
    }
}
