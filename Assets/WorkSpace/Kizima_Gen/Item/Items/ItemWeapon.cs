/*
* @file ItemWeapon.cs
* @brief •ŠíƒAƒCƒeƒ€
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : ItemBase {
    //©g‚ÌUŒ‚—Í
    private int AttackValue;

    /// <summary>
    /// ‰Šú‰»ˆ—
    /// </summary>
    public override void Initialize() {
        
    }

    /// <summary>
    /// UŒ‚—Í‚ğ“n‚·
    /// </summary>
    /// <returns></returns>
    public int GetAttackValue() {
        return AttackValue;
    }

    public override bool isWeapon() {
       return true;
    }
}
