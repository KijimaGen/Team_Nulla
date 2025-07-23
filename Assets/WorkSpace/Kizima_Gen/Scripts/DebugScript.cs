/*
 * @file DebugScript.cs
 * @brief ìÆçÏämîFóp
 * @author Sum1r3
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

using static ItemUtility;

public class DebugScript : MonoBehaviour{

    private void Start() {
        MasterdataManager.LoadAllData();
        
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            UseItem(new Vector3(0, 5, 0));
        }
        if(Input.GetMouseButtonDown(1)) {
            GetItem(0);
        }
    }

    
}
