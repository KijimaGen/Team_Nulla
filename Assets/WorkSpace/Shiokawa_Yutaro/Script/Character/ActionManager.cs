/**
 * @file ActionManager.cs
 * @brief �s���ƌ��ʂ̊Ǘ�
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
    // ���ʂ̃��X�g
    //private static List<ActionEffectBase> _effectList = null;
    //�s�����O�̃��b�Z�[�WID
    private static readonly int _USE_ACTION_LOG_ID = 1;
    public static void Initialize()
    {
        //_effectList = new List<ActionEffectBase>();

    }

    /// <summary>
    /// �s���̎��s
    /// </summary>
    /// <param name="sourceCharacter"></param>
    /// <param name="actionID"></param>
    /// <returns></returns>
    public static void ExecuteAction(CharacterBase sourceCharacter, int actionID)
    {
        // �s���̃}�X�^�[�擾
        //Entity_ActionData.Param actionMaster = GetActionMaster(actionID);
        //if (actionMaster == null) return;
        //// �˒��N���X�擾�A���s
        //ActionRangeBase range = GetRange(actionMaster.rangeType);
        //range.Execute(sourceCharacter);
        //// �A�N�V�����̌��ʏ���
        //int[] effectIDList = actionMaster.effectID;
        //for (int i = 0, max = effectIDList.Length; i < max; i++)
        //{
        //    if (effectIDList[i] < 0) continue;

        //   // await ExecuteActionEffect(effectIDList[i], sourceCharacter, range);
        //};
    }

    /// <summary>
    /// 1���ʂ̎��s
    /// </summary>
    /// <param name="effectID"></param>
    /// <param name="sourceCharacter"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    private static void ExecuteActionEffect(int effectID, CharacterBase sourceCharacter)
    {
        // ���ʂ̃}�X�^�[�擾
        //Entity_ActionEffectData.Param effectMaster = GetActionEffectMaster(effectID);
        //if (effectMaster == null) return;

        //int effectType = effectMaster.effectType;
        //if (!IsEnableIndex(_effectList, effectType)) return;
        // ���ʎ��s
        //await _effectList[effectType].Execute(sourceCharacter, effectMaster, range);
    }

}
