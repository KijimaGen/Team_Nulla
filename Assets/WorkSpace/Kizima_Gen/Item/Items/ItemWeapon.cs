/*
* @file ItemWeapon.cs
* @brief ����A�C�e��
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : ItemBase {
   
    public override void Initialize() {
        
    }

    private void Update() {
        Fall();
    }
}
