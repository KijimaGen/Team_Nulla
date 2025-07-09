/**
 * @file CharacterBase.cs
 * @brief �L�����N�^�[���̊��
 * @author yao
 * @date 2025/5/8
 */

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class CharacterBase
{
    public int ID { get; private set; } = -1;
    public int masterID { get; protected set; } = -1;
    public int posX { get; protected set; } = -1;
    public int posY { get; protected set; } = -1;
    // �}�X�^�[�f�[�^�ˑ��̕ϐ�
    public int nameID { get; protected set; } = -1;
    public int maxHP { get; protected set; } = -1;
    public int HP { get; protected set; } = -1;
    // ���S���Ă��邩
    public bool isDead { get { return HP <= 0; } }
    public int rawAttack { get; protected set; } = -1;
    public int rawDefense { get; protected set; } = -1;

    public List<int> possessItemList { get; protected set; } = null;
    //�����A�C�e���̍ő吔
    private static readonly int _POSSESS_ITEM_MAX = 10;

    /// <summary>
    /// �g�p�O�̏���
    /// </summary>
    /// <param name="setID"></param>
    /// <param name="squareData"></param>
    /// <param name="setMasterID"></param>
    //public virtual void Setup(int setID,int setMasterID)
    //{
    //    ID = setID;
    //    masterID = setMasterID;
    //    var characterMaster = CharacterMasterUtility.GetCharacterMaster(masterID);
    //    SetupMaster(characterMaster);
    //    // �I�u�W�F�N�g�̏���
    //    GetObject()?.Setup(characterMaster.spriteName);
    //    // �Ƃ肠����������������
    //    SetDirection(eDirectionEight.Down);
    //    possessItemList = new List<int>(_POSSESS_ITEM_MAX);
    //}

    /// <summary>
    /// �}�X�^�[�f�[�^�֘A�̏���
    /// </summary>
    /// <param name="setMasterID"></param>
    //protected virtual void SetupMaster(Entity_CharacterData.Param characterMaster)
    //{
    //    if (characterMaster == null) return;

    //    nameID = characterMaster.nameID;
    //    SetMaxHP(characterMaster.HP);
    //    SetHP(maxHP);
    //    SetRawAttack(characterMaster.Attack);
    //    SetRawDefense(characterMaster.Defense);
    //}

    /// <summary>
    /// �g�p��̕Еt��
    /// </summary>
    public virtual void Teardown()
    {
        // ������}�X�����菜��
       // GetSquareData(posX, posY)?.RemoveCharacter();
        posX = -1;
        posY = -1;
        // �I�u�W�F�N�g�̕Еt��
       // GetObject()?.Teardown();
    }

    /// <summary>
    /// �I�u�W�F�N�g�̎擾
    /// </summary>
    /// <returns></returns>
    //protected CharacterObject GetObject()
    //{
    //    return CharacterManager.instance.GetCharacterObject(ID);
    //}
    /// <summary>
    /// �����ڂ݂̂̈ʒu�ύX
    /// </summary>
    /// <param name="position"></param>
    public virtual void SetPosition(Vector3 position)
    {
        // �L�����N�^�[�I�u�W�F�N�g���擾���ʒu�ύX����
       // GetObject()?.SetPosition(position);
    }

    /// <summary>
    /// �v���C���[���ۂ�
    /// </summary>
    /// <returns></returns>
    public abstract bool IsPlayer();

    /// <summary>
    /// �s���̎v�l
    /// </summary>
    public virtual void ThinkAction()
    {

    }

    /// <summary>
    /// �t���A�I��������
    /// </summary>
    //public virtual UniTask OnEndFloor()
    //{
    //    return UniTask.CompletedTask;
    //}

    /// <summary>
    /// �ړ��̋O�ՂɊ܂܂�Ă��邩
    /// </summary>
    /// <param name="squareID"></param>
    /// <returns></returns>
    public virtual bool ExistMoveTrail(int squareID)
    {
        return false;
    }

    /// <summary>
    /// �L�����̌����ݒ�
    /// </summary>
    /// <param name="setDir"></param>
    //public void SetDirection(eDirectionEight setDir)
    //{
    //    if (direction == setDir) return;

    //    direction = setDir;
    //    GetObject()?.SetDirection(direction);
    //}

    /// <summary>
    /// �U���͎擾
    /// </summary>
    /// <returns></returns>
    public int GetAttack()
    {
        return rawAttack;
    }

    /// <summary>
    /// �f�̍U���͐ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetRawAttack(int setValue)
    {
        rawAttack = setValue;
    }

    /// <summary>
    /// �h��͎擾
    /// </summary>
    /// <returns></returns>
    public int GetDefense()
    {
        return rawDefense;
    }

    /// <summary>
    /// �f�̖h��͐ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetRawDefense(int setValue)
    {
        rawDefense = setValue;
    }

    /// <summary>
    /// �ő�HP�ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetMaxHP(int setValue)
    {
        maxHP = setValue;
    }

    /// <summary>
    /// ����HP�ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetHP(int setValue)
    {
        // 0�`�ő�l�Ɋۂ߂�
        HP = Mathf.Clamp(setValue, 0, maxHP);
    }

    /// <summary>
    /// HP��
    /// </summary>
    /// <param name="addValue"></param>
    public void AddHP(int addValue)
    {
        SetHP(HP + addValue);
    }

    /// <summary>
    /// HP����
    /// </summary>
    /// <param name="removeValue"></param>
    public void RemoveHP(int removeValue)
    {
        SetHP(HP - removeValue);
    }

    /// <summary>
    /// �L�����N�^�[�̎��S
    /// </summary>
    public abstract void Dead();

    /// <summary>
    /// �\���p�����x�擾
    /// </summary>
    /// <returns></returns>
    public virtual int GetShowStamina()
    {
        return 0;
    }

    /// <summary>
    /// �����x�擾
    /// </summary>
    /// <returns></returns>
    public virtual int GetStamina()
    {
        return 0;
    }

    /// <summary>
    /// �����x�ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetStamina(int setValue)
    {

    }

    /// <summary>
    /// �����x����
    /// </summary>
    /// <param name="addValue"></param>
    public void AddStamina(int addValue)
    {
        SetStamina(GetStamina() + addValue);
    }

    /// <summary>
    /// �����x����
    /// </summary>
    /// <param name="removeValue"></param>
    public void RemoveStamina(int removeValue)
    {
        SetStamina(GetStamina() - removeValue);
    }

    /// <summary>
    /// �A�j���[�V�����̍Đ�
    /// </summary>
    /// <param name="setAnim"></param>
    //public void SetAnimation(eCharacterAnimation setAnim)
    //{
    //    GetObject()?.SetAnimation(setAnim);
    //}

    /// <summary>
    /// �Đ����̃A�j���[�V�����擾
    /// </summary>
    /// <returns></returns>
    //public eCharacterAnimation GetCurrentAnimation()
    //{
    //    CharacterObject characterObject = GetObject();
    //    if (characterObject == null) return eCharacterAnimation.Invalid;

    //    return characterObject.currentAnim;
    //}

    /// <summary>
    /// �\��s���̎��s
    /// </summary>
    /// <returns></returns>
    //public virtual async UniTask ExecuteScheduleAction()
    //{
    //    await UniTask.CompletedTask;
    //}

    /// <summary>
    /// �\��s���̃��Z�b�g
    /// </summary>
    public virtual void ResetScheduleAction() { }

    /// <summary>
    /// �^�[���I�����̏���
    /// </summary>
    public virtual void OnEndTurn()
    {

    }

    public bool CanAddItem()
    {
        return possessItemList.Count < _POSSESS_ITEM_MAX;
    }

    public void AddItem(int addItemID)
    {
        possessItemList.Add(addItemID);
    }
    public void RemoveItem(int removeItemID)
    {
        possessItemList.Remove(removeItemID);
    }
}

