/**
 * @file ActionManager.cs
 * @brief 行動と効果の管理
 * @author yao
 * @date 2025/6/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;
using Unity.VisualScripting;

public class ActionManager
{
    // 効果のリスト
    //private static List<ActionEffectBase> _effectList = null;
    //行動ログのメッセージID
    private static readonly int _USE_ACTION_LOG_ID = 1;
    public static void Initialize()
    {
        //_effectList = new List<ActionEffectBase>();

    }

    /// <summary>
    /// 行動の実行
    /// </summary>
    /// <param name="sourceCharacter"></param>
    /// <returns></returns>
    public static void ExecuteAction(CharacterBase sourceCharacter)
    {
        
    }

    /// <summary>
    /// 1効果の実行
    /// </summary>
    /// <param name="effectID"></param>
    /// <param name="sourceCharacter"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    private static void ExecuteActionEffect(int effectID, CharacterBase sourceCharacter)
    {
        // 効果のマスター取得
        //Entity_ActionEffectData.Param effectMaster = GetActionEffectMaster(effectID);
        //if (effectMaster == null) return;

        //int effectType = effectMaster.effectType;
        //if (!IsEnableIndex(_effectList, effectType)) return;
        // 効果実行
        //await _effectList[effectType].Execute(sourceCharacter, effectMaster, range);
    }

}
