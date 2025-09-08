/*
 * @file AudioManager.cs
 * @brief ‰¹‚Ìˆ—‚Ü‚Æ‚ß
 * @author kijima
 * @date 2025/9/9
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SystemObject{
    //
    [SerializeField]
    private AudioSource SESource;
    [SerializeField]
    private List<AudioClip> SEClipList;
    [SerializeField]
    private AudioSource BGMSource;
    [SerializeField]
    private List<AudioClip> BGMClipList;

    public static AudioManager instance;

    public override void Initialize() {
       instance = this;
    }


    /// <summary>
    /// Œø‰Ê‰¹‚ÌÄ¶
    /// </summary>
    /// <param name="SEindex"></param>
    public void PlaySE(int SEindex) {
        if (SEClipList.Count < SEindex) return;
        SESource.PlayOneShot(SEClipList[SEindex]);
    }
}
