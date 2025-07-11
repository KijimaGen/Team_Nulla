using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _characterSprite = null;
    // ���ݍĐ����̃A�j���[�V����
    public eCharacterAnimation currentAnim { get; private set; } = eCharacterAnimation.Invalid;
    /// <summary>
    /// �g�p�O�̏���
    /// </summary>
    public void Setup(int modelName)
    {
        // �ҋ@�A�j���[�V������ݒ�
        SetAnimation(eCharacterAnimation.Wait);
    }

    /// <summary>
    /// �ʒu�̐ݒ�
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// �A�j���[�V�����̍Đ�
    /// </summary>
    /// <param name="setAnim"></param>
    public void SetAnimation(eCharacterAnimation setAnim)
    {
        // ���݂Ɠ����A�j���[�V�����Ȃ珈�����Ȃ�
        if (currentAnim == setAnim) return;

        currentAnim = setAnim;
    }
}