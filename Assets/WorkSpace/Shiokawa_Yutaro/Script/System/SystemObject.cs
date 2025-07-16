/*
* @file SystemObject.cs
* @brief ほぼすべてのマネージャーの基底
* @author kijima
* @date 2025/7/9
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SystemObject : MonoBehaviour{

    /// <summary>
    /// 初期化処理
    /// </summary>
    public abstract void Initialize();
    
}
