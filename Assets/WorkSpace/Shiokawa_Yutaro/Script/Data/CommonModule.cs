/**
 * @file CommonModule.cs
 * @brief ���p�����N���X
 * @author yao
 * @date 2025/4/15
 */

using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;

public class CommonModule
{

    /// <summary>
    /// ���X�g���󂩔���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(List<T> list)
    {
        // �Z���]���Ȃ̂ő��v
        return list == null || list.Count <= 0;
    }

    public static bool IsEmpty<T>(T[] array)
    {
        return array == null || array.Length <= 0;
    }

    /// <summary>
    /// ���X�g�ɑ΂��ėL���ȃC���f�N�X������
    /// </summary>
    /// <returns></returns>
    public static bool IsEnableIndex<T>(List<T> list, int index)
    {
        if (IsEmpty(list)) return false;

        return index >= 0 && list.Count > index;
    }

    public static bool IsEnableIndex<T>(T[] array, int index)
    {
        if (IsEmpty(array)) return false;

        return index >= 0 && array.Length > index;
    }

    /// <summary>
    /// ���X�g������������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="capacity"></param>
    public static void InitializeList<T>(ref List<T> list, int capacity = -1)
    {
        if (list == null)
        {
            if (capacity < 1)
            {
                list = new List<T>();
            }
            else
            {
                list = new List<T>(capacity);
            }
        }
        else
        {
            if (list.Capacity < capacity) list.Capacity = capacity;

            list.Clear();
        }
    }

    /// <summary>
    /// ���X�g���d���Ȃ��Ń}�[�W
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    public static void MeargeList<T>(ref List<T> main, List<T> sub)
    {
        if (IsEmpty(sub)) return;

        int meargeCount = sub.Count;
        if (main == null) main = new List<T>(meargeCount);

        for (int i = 0; i < meargeCount; i++)
        {
            // �d�������v�f�͒ǉ����Ȃ�
            if (main.Exists(mainElem => mainElem.Equals(sub[i]))) continue;

            main.Add(sub[i]);
        }
    }

    /// <summary>
    /// �����̃^�X�N�̏I����҂�
    /// </summary>
    /// <param name="taskList"></param>
    /// <returns></returns>
    //public static async UniTask WaitTask(List<UniTask> taskList)
    //{
    //    // �I�������^�X�N�����X�g���珜���A���X�g����ɂȂ�܂ő҂�
    //    while (!IsEmpty(taskList))
    //    {
    //        // �r���ŗv�f��������\��������̂Ŗ������瑖��
    //        for (int i = taskList.Count - 1; i >= 0; i--)
    //        {
    //            if (!taskList[i].Status.IsCompleted()) continue;
    //            // �^�X�N���I�����Ă����烊�X�g���甲��
    //            taskList.RemoveAt(i);
    //        }
    //        await UniTask.DelayFrame(1);
    //    }
    //}

    //public static async UniTask WaitTask(List<UniTask> taskList, CancellationToken token)
    //{

    //    // �I�������^�X�N�����X�g���珜���A���X�g����ɂȂ�܂ő҂�
    //    while (!IsEmpty(taskList))
    //    {
    //        // �r���ŗv�f��������\��������̂Ŗ������瑖��
    //        for (int i = taskList.Count - 1; i >= 0; i--)
    //        {
    //            if (!taskList[i].Status.IsCompleted()) continue;
    //            // �^�X�N���I�����Ă����烊�X�g���甲��
    //            taskList.RemoveAt(i);
    //        }
    //        await UniTask.DelayFrame(1, PlayerLoopTiming.Update, token);
    //    }
    //}
}
