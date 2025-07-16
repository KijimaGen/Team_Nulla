/**
 * @file CharacterBase.cs
 * @brief �L�����N�^�[���̊��
 * @author yao
 * @date 2025/5/8
 */

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class CharacterBase : MonoBehaviour
{
    public int ID { get; private set; } = -1;

    // �}�X�^�[�f�[�^�ˑ��̕ϐ�
    public int nameID { get; protected set; } = -1;
    public float maxHP { get; protected set; } = -1;
    public float HP { get; protected set; } = -1;
    // ���S���Ă��邩
    public bool isDead { get { return HP <= 0; } }
    public float rawAttack { get; protected set; } = -1;
    public float rawDefense { get; protected set; } = -1;
    public float speed { get; protected set; } = -1;

    public List<int> possessItemList { get; protected set; } = null;
    //�����A�C�e���̍ő吔
    private static readonly int _POSSESS_ITEM_MAX = 6;

    protected Animator animator;
    protected Rigidbody rb;

    protected bool isAttacking = false;

    private void Start()
    {
        Setup();
    }

    /// <summary>
    /// �g�p�O�̏���
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// �}�X�^�[�f�[�^�֘A�̏���
    /// </summary>
    /// <param name="setMasterID"></param>
    protected void SetStatus()
    {
        SetMaxHP(maxHP);
        SetHP(HP);
        SetRawAttack(rawAttack);
        SetRawDefense(rawDefense);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }



    /// <summary>
    /// �v���C���[���ۂ�
    /// </summary>
    /// <returns></returns>
    public abstract bool IsPlayer();


    /// <summary>
    /// �t���A�I��������
    /// </summary>
    //public virtual UniTask OnEndFloor()
    //{
    //    return UniTask.CompletedTask;
    //}

    /// <summary>
    /// �U���͎擾
    /// </summary>
    /// <returns></returns>
    public float GetAttack()
    {
        return rawAttack;
    }

    /// <summary>
    /// �f�̍U���͐ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetRawAttack(float setValue)
    {
        rawAttack = setValue;
    }

    /// <summary>
    /// �h��͎擾
    /// </summary>
    /// <returns></returns>
    public float GetDefense()
    {
        return rawDefense;
    }

    /// <summary>
    /// �f�̖h��͐ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetRawDefense(float setValue)
    {
        rawDefense = setValue;
    }

    /// <summary>
    /// �ő�HP�ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetMaxHP(float setValue)
    {
        maxHP = setValue;
    }

    /// <summary>
    /// ����HP�ݒ�
    /// </summary>
    /// <param name="setValue"></param>
    public virtual void SetHP(float setValue)
    {
        // 0�`�ő�l�Ɋۂ߂�
        HP = Mathf.Clamp(setValue, 0, maxHP);
    }

    /// <summary>
    /// HP��
    /// </summary>
    /// <param name="addValue"></param>
    public void AddHP(float addValue)
    {
        SetHP(HP + addValue);
    }

    /// <summary>
    /// HP����
    /// </summary>
    /// <param name="removeValue"></param>
    public void RemoveHP(float removeValue)
    {
        SetHP(HP - removeValue);
    }

    /// <summary>
    /// �L�����N�^�[�̎��S
    /// </summary>
    public abstract void Dead();

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

    protected bool CheckGrounded(Vector3 dir)
    {
        Vector3 origin1 = (transform.position + dir * 2f);
        Vector3 origin2 = (transform.position + dir * 1.5f);
        Vector3 origin3 = (transform.position + dir * 1f);
        Vector3 origin4 = (transform.position + dir * 0.5f);
        Vector3 direction = Vector3.down;
        float rayLength = 2f;
        //int rayCount = 3;
        int groundLayer = LayerMask.GetMask("Ground");

        //for (int i = 0; i < rayCount; i++)
        //{
        //    Debug.DrawRay(origin, direction * rayLength, Color.red);
        //    return Physics.Raycast(origin , direction, rayLength, groundLayer);
        //}

        Debug.DrawRay(origin1, direction * rayLength, Color.red);
        Debug.DrawRay(origin2, direction * rayLength, Color.red);
        Debug.DrawRay(origin3, direction * rayLength, Color.red);

        return Physics.Raycast(origin1, direction, rayLength, groundLayer)
                 && Physics.Raycast(origin2, direction, rayLength, groundLayer)
                     && Physics.Raycast(origin3, direction, rayLength, groundLayer)
                         && Physics.Raycast(origin4, direction, rayLength, groundLayer);
    }

    protected void Attack(string attackName)
    {
       // rb.velocity = Vector3.zero;
        //animator.SetBool("run", false);
        //animator.SetTrigger("attack");
        //isAttacking = true;
        //playAnim = true;
    }

    public void IsAttackingOFF()
    {
        isAttacking = false;
    }
}

