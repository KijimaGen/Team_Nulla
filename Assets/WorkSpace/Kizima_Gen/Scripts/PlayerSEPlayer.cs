/*
* @file ParkManager.cs
* @brief ƒp[ƒN‚ÌŠÇ—Ò
* @author kijima
* @date 2025/7/16
*/
using UnityEngine;
public class PlayerSEPlayer : MonoBehaviour{




    /// <summary>
    /// •à‚­‚Æ‚«‚ÌŒø‰Ê‰¹
    /// </summary>
    public void PlayWalkSE() {
        AudioManager.instance.PlaySE(11);
    }

    /// <summary>
    /// ‘–‚é‚Æ‚«‚ÌŒø‰Ê‰¹
    /// </summary>
    public void PlayDashSE() {
        AudioManager.instance.PlaySE(12);
    }
}
