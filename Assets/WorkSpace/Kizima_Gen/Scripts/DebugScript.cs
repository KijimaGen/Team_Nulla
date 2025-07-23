/*
 * @file DebugScript.cs
 * @brief ìÆçÏämîFóp
 * @author Sum1r3
 * @date 2025/7/9
 */
using UnityEngine;

using static ItemUtility;

public class DebugScript : MonoBehaviour{

    private void Start() {
        MasterdataManager.LoadAllData();
        
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            UseItem(new Vector3(57, 5, 59));
        }
        if(Input.GetMouseButtonDown(1)) {
            GetItem(0);
        }
    }

    
}
