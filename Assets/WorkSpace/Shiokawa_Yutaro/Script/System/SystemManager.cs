/*
* @file SystemManager.cs
* @brief マネージャー管理マネージャー(大体ここがエントリーポイント)
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
        // 全システムオブジェクトの生成、初期化
        for (int i = 0, max = systemObjectList.Count; i < max; i++) {
            SystemObject origin = systemObjectList[i];
            if (origin == null) continue;
            // システムオブジェクト生成
            SystemObject createObject = Instantiate(origin, transform);
            // 初期化
            createObject.Initialize();
        }
        
    }
}
