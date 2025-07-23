/*
* @file ParkMasterUtility.cs
* @brief パークのマスターデータ実行処理
* @author kijima
* @date 2025/7/16
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkMasterUtility {
    /// <summary>
    /// ID指定のパークマスター取得
    /// </summary>
    /// <param name="masterID"></param>
    /// <returns></returns>
    public static Entity_Park.Param GetParkMaster(int masterID) {
        var parkMasterList = MasterdataManager.parkData[0];
        for (int i = 0, max = parkMasterList.Count; i < max; i++) {
            if (parkMasterList[i].ID != masterID) continue;
            return parkMasterList[i];
        }
        return null;
    }
}
