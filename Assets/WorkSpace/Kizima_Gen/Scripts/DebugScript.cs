/*
 * @file DebugScript.cs
 * @brief ìÆçÏämîFóp
 * @author Sum1r3
 * @date 2025/7/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ItemUtility;

public class DebugScript : MonoBehaviour{

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            UseItem();
        }
        if(Input.GetMouseButtonDown(1)) {
            UnuseItem(0);
        }
    }

    public static void ActionTrigger() {
        UseItem();

    }
}
