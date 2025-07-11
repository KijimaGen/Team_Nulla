using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _characterSprite = null;
    // 現在再生中のアニメーション
    public eCharacterAnimation currentAnim { get; private set; } = eCharacterAnimation.Invalid;
    /// <summary>
    /// 使用前の準備
    /// </summary>
    public void Setup(int modelName)
    {
        // 待機アニメーションを設定
        SetAnimation(eCharacterAnimation.Wait);
    }

    /// <summary>
    /// 位置の設定
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// アニメーションの再生
    /// </summary>
    /// <param name="setAnim"></param>
    public void SetAnimation(eCharacterAnimation setAnim)
    {
        // 現在と同じアニメーションなら処理しない
        if (currentAnim == setAnim) return;

        currentAnim = setAnim;
    }
}