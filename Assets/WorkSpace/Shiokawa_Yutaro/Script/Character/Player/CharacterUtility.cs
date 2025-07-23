using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CharacterUtility
{

    /// <summary>
    /// エネミー生成
    /// </summary>
    /// <param name="squareData"></param>
    /// <param name="masterID"></param>
    public static void UseEnemy(int masterID)
    {
       // CharacterManager.instance.UseEnemy(masterID);
    }

    /// <summary>
    /// プレイヤー削除
    /// </summary>
    /// <param name="unusePlayer"></param>
    public static void UnusePlayer(PlayerCharacter unusePlayer)
    {
       // CharacterManager.instance.UnusePlayer(unusePlayer);
    }

    /// <summary>
    /// エネミー削除
    /// </summary>
    /// <param name="unuseEnemy"></param>
    //public static void UnuseEnemy(EnemyCharacter unuseEnemy)
    //{
    //   // CharacterManager.instance.UnuseEnemy(unuseEnemy);
    //}

}
