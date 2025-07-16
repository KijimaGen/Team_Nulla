using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using static CommonModule;

/**
 * @file CharacterManager.cs
 * @brief �L�����N�^�[�Ǘ�
 * @author yao
 * @date 2025/5/8
 */
public class CharacterManager : MonoBehaviour
{
    /// ���g�ւ̎Q��
    public static CharacterManager instance { get; private set; } = null;

    // �g�p���L�����N�^�[�I�u�W�F�N�g�̐e�I�u�W�F�N�g
    [SerializeField]
    private Transform _useObjectRoot = null;
    // ���g�p�L�����N�^�[�I�u�W�F�N�g�̐e�I�u�W�F�N�g
    [SerializeField]
    private Transform _unuseObjectRoot = null;
    /// �L�����N�^�[�I�u�W�F�N�g�̃I���W�i��
    [SerializeField]
    private CharacterBase _originObject = null;

    // ���g�p��Ԃ̃G�l�~�[���X�g
    //private List<EnemyCharacter> _unuseEnemyList = null;

    // �g�p���̃L�����N�^�[�I�u�W�F�N�g���X�g
    private List<CharacterBase> _useObjectList = null;
    // ���g�p��Ԃ̃L�����N�^�[�I�u�W�F�N�g���X�g
    private List<CharacterBase> _unuseObjectList = null;

    private const int FLOOR_ENEMY_MAX = 5;

    public void Start()
    {
        instance = this;



        //_unuseEnemyList = new List<EnemyCharacter>(FLOOR_ENEMY_MAX);
        //for (int i = 0; i < FLOOR_ENEMY_MAX; i++)
        //{
        //    _unuseEnemyList.Add(new EnemyCharacter());
        //}
        // �L�����N�^�[�I�u�W�F�N�g��K�v���������Ė��g�p��Ԃɂ��Ă���
        _useObjectList = new List<CharacterBase>(FLOOR_ENEMY_MAX + 1);
        _unuseObjectList = new List<CharacterBase>(FLOOR_ENEMY_MAX + 1);
        for (int i = 0; i < FLOOR_ENEMY_MAX + 1; i++)
        {
            _unuseObjectList.Add(Instantiate(_originObject, _unuseObjectRoot));
        }

    }

    ///// <summary>
    ///// �G�l�~�[�L�����N�^�[�̐���
    ///// </summary>
    ///// <param name="squareData"></param>
    ///// <param name="masterID"></param>
    //public void UseEnemy(MapSquareData squareData, int masterID)
    //{
    //    // �G�l�~�[���̃C���X�^���X�̎擾
    //    EnemyCharacter useEnemy;
    //    if (IsEmpty(_unuseEnemyList))
    //    {
    //        // ���g�p���Ȃ��̂Ő���
    //        useEnemy = new EnemyCharacter();
    //    }
    //    else
    //    {
    //        // ���g�p���X�g����g��
    //        useEnemy = _unuseEnemyList[0];
    //        _unuseEnemyList.RemoveAt(0);
    //    }
    //    UseCharacter(useEnemy, squareData, masterID);
    //}

    ///// <summary>
    ///// �L�����N�^�[���g�p��Ԃɂ���
    ///// </summary>
    ///// <param name="useCharacter"></param>
    ///// <param name="squareData"></param>
    ///// <param name="masterID"></param>
    //private void UseCharacter(CharacterBase useCharacter, MapSquareData squareData, int masterID)
    //{
    //    // �g�p�\��ID���擾���Ďg�p���X�g�ɒǉ�
    //    int useID = -1;
    //    for (int i = 0, max = _useList.Count; i < max; i++)
    //    {
    //        if (_useList[i] != null) continue;
    //        // �g�p���X�g�Ɏg����ꏊ����������
    //        useID = i;
    //        _useList[i] = useCharacter;
    //        break;
    //    }
    //    if (useID < 0)
    //    {
    //        // �g�p���X�g�Ɏg����ꏊ���Ȃ������̂Ŗ����ɒǉ�
    //        useID = _useList.Count;
    //        _useList.Add(useCharacter);
    //    }
    //    // �I�u�W�F�N�g�̎擾
    //    CharacterObject useObject = null;
    //    if (IsEmpty(_unuseObjectList))
    //    {
    //        // ���g�p���Ȃ��̂Ő���
    //        useObject = Instantiate(_originObject);
    //    }
    //    else
    //    {
    //        // ���g�p���X�g����g��
    //        useObject = _unuseObjectList[0];
    //        _unuseObjectList.RemoveAt(0);
    //    }
    //    // �I�u�W�F�N�g�̎g�p���X�g�ւ̒ǉ�
    //    while (!IsEnableIndex(_useObjectList, useID)) _useObjectList.Add(null);

    //    _useObjectList[useID] = useObject;
    //    useObject.transform.SetParent(_useObjectRoot);

    //    // �C���X�^���X�̎g�p����������
    //    useCharacter.Setup(useID, squareData, masterID);
    //}

    ///// <summary>
    ///// �v���C���[�𖢎g�p��Ԃɂ���
    ///// </summary>
    ///// <param name="unusePlayer"></param>
    //public void UnusePlayer(PlayerCharacter unusePlayer)
    //{
    //    if (unusePlayer == null) return;
    //    // ���g�p���X�g�ɉ�����
    //    _unusePlayerList.Add(unusePlayer);
    //    UnuseCharacter(unusePlayer);
    //}

    ///// <summary>
    ///// �G�l�~�[�𖢎g�p��Ԃɂ���
    ///// </summary>
    ///// <param name="unuseEnemy"></param>
    //public void UnuseEnemy(EnemyCharacter unuseEnemy)
    //{
    //    if (unuseEnemy == null) return;
    //    // ���g�p���X�g�ɉ�����
    //    _unuseEnemyList.Add(unuseEnemy);
    //    UnuseCharacter(unuseEnemy);
    //}

    /// <summary>
    /// �L�����N�^�[�𖢎g�p��Ԃɂ���
    /// </summary>
    /// <param name="unuseCharacter"></param>
    //private void UnuseCharacter(CharacterBase unuseCharacter)
    //{
    //    // �g�p���X�g�����菜��
    //    int unuseID = unuseCharacter.ID;
    //    _useList[unuseID] = null;
    //    // �Еt���������Ă�
    //    unuseCharacter.Teardown();
    //    // �I�u�W�F�N�g�𖢎g�p�ɂ���
    //    CharacterObject unuseObject = GetCharacterObject(unuseID);
    //    _useObjectList[unuseID] = null;
    //    _unuseObjectList.Add(unuseObject);
    //    unuseObject.transform.SetParent(_unuseObjectRoot);
    //}

    /// <summary>
    /// �S�ẴL�����N�^�[�Ɏw�菈�����s
    /// </summary>
    /// <param name="action"></param>
    //public void ExecuteAllCharacter(System.Action<CharacterBase> action)
    //{
    //    if (action == null || IsEmpty(_useList)) return;

    //    for (int i = 0, max = _useList.Count; i < max; i++)
    //    {
    //        if (_useList[i] == null) continue;

    //        action(_useList[i]);
    //    }
    //}

    /// <summary>
    /// �S�ẴL�����N�^�[�Ɏw��^�X�N���s
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    //public async UniTask ExecuteTaskAllCharacter(System.Func<CharacterBase, UniTask> task)
    //{
    //    if (task == null || IsEmpty(_useList)) return;

    //    for (int i = 0, max = _useList.Count; i < max; i++)
    //    {
    //        if (_useList[i] == null) continue;

    //        await task(_useList[i]);
    //    }
    //}

}
