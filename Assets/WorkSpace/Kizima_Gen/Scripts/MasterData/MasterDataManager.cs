/**
 * @file MasterdataManager.cs
 * @brief �}�X�^�[�f�[�^�Ǘ�
 * @author Sum1r3
 * @date 2025/7/15
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class MasterdataManager {
    //�}�X�^�[�f�[�^�̃t�@�C���p�X
    private static readonly string _DATA_PATH = "MasterData/";
    //�ǂݍ��񂾃}�X�^�[�f�[�^
    public static List<List<Entity_Park.Param>> parkData = null;


    /// <summary>
    /// �S�Ẵ}�X�^�[�f�[�^��ǂݍ���
    /// </summary>
    public static void LoadAllData() {
        parkData = Load<Entity_Park, Entity_Park.Sheet, Entity_Park.Param>("ParkMaster");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="dataName">ScriptableObject�t�@�C����</param>
    /// <returns></returns>                                        ���W�F�l���b�N�N���XT1��ScriptableObject���p�������N���X�Ɍ�����
    private static List<List<T3>> Load<T1, T2, T3>(string dataName) where T1 : ScriptableObject {
        //�t�@�C����ǂݍ���
        T1 sourceData = Resources.Load<T1>(_DATA_PATH + dataName);
        //���̎w��ŃV�[�g���擾
        FieldInfo sheetField = typeof(T1).GetField("sheets");
        List<T2> sheetListData = sheetField.GetValue(sourceData) as List<T2>;

        //���̎w��Ńt�B�[���h���擾
        FieldInfo listField = typeof(T2).GetField("list");
        List<List<T3>> paramList = new List<List<T3>>();
        foreach (object elem in sheetListData) {
            List<T3> param = listField.GetValue(elem) as List<T3>;
            paramList.Add(param);
        }

        return paramList;
    }
}
