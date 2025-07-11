using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMasterUtility
{
    /// <summary>
    /// ID�w��̃L�����N�^�[�}�X�^�[�擾
    /// </summary>
    /// <param name="masterID"></param>
    /// <returns></returns>
    public static Entity_CharacterData.Param GetCharacterMaster(int masterID)
    {
        // �L�����N�^�[�}�X�^�[�f�[�^�擾
        var characterMasterList = MasterDataManager.characterData[0];
        // ID����v������̂�Ԃ�
        for (int i = 0, max = characterMasterList.Count; i < max; i++)
        {
            if (characterMasterList[i].ID != masterID) continue;

            return characterMasterList[i];
        }
        return null;
    }

}
