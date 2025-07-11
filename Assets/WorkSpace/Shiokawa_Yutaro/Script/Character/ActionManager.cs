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
    /// <param name="actionID"></param>
    /// <returns></returns>
    public static void ExecuteAction(CharacterBase sourceCharacter, int actionID)
    {
        // 行動のマスター取得
        //Entity_ActionData.Param actionMaster = GetActionMaster(actionID);
        //if (actionMaster == null) return;
        //// 射程クラス取得、実行
        //ActionRangeBase range = GetRange(actionMaster.rangeType);
        //range.Execute(sourceCharacter);
        //// アクションの効果処理
        //int[] effectIDList = actionMaster.effectID;
        //for (int i = 0, max = effectIDList.Length; i < max; i++)
        //{
        //    if (effectIDList[i] < 0) continue;

        //   // await ExecuteActionEffect(effectIDList[i], sourceCharacter, range);
        //};
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
