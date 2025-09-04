/*
 * @file DebugScript.cs
 * @brief 動作確認用
 * @author Sum1r3
 * @date 2025/7/9
 */
using UnityEngine;

using static ItemUtility;

public class DebugScript : MonoBehaviour{
    public static DebugScript instance;

    //立体音響お試し用
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clip;

    //普通に効果音鳴らしたい
    [SerializeField]
    private AudioSource audioSource2;
    [SerializeField]
    private AudioClip clip2;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }

        //ここで3D音響にする
        audioSource.spatialBlend = 1.0f;

        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 20f;
        MasterdataManager.LoadAllData();
    }

    public void PlaySound() {
        audioSource.PlayOneShot(clip);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlaySE() {
        audioSource2.PlayOneShot(clip2);
    }
    
}
