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
    private CharacterObject _originObject = null;

    // �g�p���̃L�����N�^�[���X�g
    private List<CharacterBase> _useList = null;
    // ���g�p��Ԃ̃v���C���[���X�g
    //private List<PlayerCharacter> _unusePlayerList = null;
    //// ���g�p��Ԃ̃G�l�~�[���X�g
    //private List<EnemyCharacter> _unuseEnemyList = null;

    // �g�p���̃L�����N�^�[�I�u�W�F�N�g���X�g
    private List<CharacterObject> _useObjectList = null;
    // ���g�p��Ԃ̃L�����N�^�[�I�u�W�F�N�g���X�g
    private List<CharacterObject> _unuseObjectList = null;

    public void Start()
    {
        instance = this;
        //// �L�����N�^�[����K�v���������Ė��g�p��Ԃɂ��Ă���
        //_useList = new List<CharacterBase>(FLOOR_ENEMY_MAX + 1);

        //_unusePlayerList = new List<PlayerCharacter>(1);
        //_unusePlayerList.Add(new PlayerCharacter());

        //_unuseEnemyList = new List<EnemyCharacter>(FLOOR_ENEMY_MAX);
        //for (int i = 0; i < FLOOR_ENEMY_MAX; i++)
        //{
        //    _unuseEnemyList.Add(new EnemyCharacter());
        //}
        //// �L�����N�^�[�I�u�W�F�N�g��K�v���������Ė��g�p��Ԃɂ��Ă���
        //_useObjectList = new List<CharacterObject>(FLOOR_ENEMY_MAX + 1);
        //_unuseObjectList = new List<CharacterObject>(FLOOR_ENEMY_MAX + 1);
        //for (int i = 0; i < FLOOR_ENEMY_MAX + 1; i++)
        //{
        //    _unuseObjectList.Add(Instantiate(_originObject, _unuseObjectRoot));
        //}
    }

    ///// <summary>
    ///// �v���C���[�L�����N�^�[����
    ///// </summary>
    ///// <param name="squareData"></param>
    ///// <param name="masterID"></param>
    //public void UsePlayer(MapSquareData squareData, int masterID)
    //{
    //    // �L�����N�^�[���̃C���X�^���X�̎擾
    //    PlayerCharacter usePlayer;
    //    if (IsEmpty(_unusePlayerList))
    //    {
    //        // ���g�p���Ȃ��̂Ő���
    //        usePlayer = new PlayerCharacter();
    //    }
    //    else
    //    {
    //        // ���g�p���X�g����g��
    //        usePlayer = _unusePlayerList[0];
    //        _unusePlayerList.RemoveAt(0);
    //    }
    //    // �g�p�\��ID�����蓖�Ďg�p��Ԃɂ���
    //    UseCharacter(usePlayer, squareData, masterID);
    //}

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

    /// <summary>
    /// ID�w��̃L�����N�^�[�f�[�^�擾
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public CharacterBase GetCharacterData(int ID)
    {
        if (!IsEnableIndex(_useList, ID)) return null;

        return _useList[ID];
    }

    /// <summary>
    /// ID�w��̃L�����N�^�[�I�u�W�F�N�g�擾
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public CharacterObject GetCharacterObject(int ID)
    {
        if (!IsEnableIndex(_useObjectList, ID)) return null;

        return _useObjectList[ID];
    }

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
    /// �v���C���[�擾
    /// </summary>
    /// <returns></returns>
    //public CharacterBase GetPlayer()
    //{
    //    if (IsEmpty(_useList)) return null;

    //    for (int i = 0, max = _useList.Count; i < max; i++)
    //    {
    //        // �v���C���[���ۂ�����
    //        if (_useList[i] == null ||
    //            !_useList[i].IsPlayer()) continue;

    //        return _useList[i];
    //    }
    //    return null;
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
