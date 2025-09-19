/**
 * @file FadeManager.cs
 * @brief フェードの管理クラス
 * @author kijima
 * @date 2025/9/8
 */

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : SystemObject {
    //フェード用黒画像
    [SerializeField]
    private Image _fadeImage;
    public static FadeManager instance { get; private set; } = null;

    //どのくらいの時間をかけてフェードインフェードアウトするか
    private const float _DEFAULT_FADE_DURATION = 0.5f;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <returns></returns>
    public override void Initialize() {
        instance = this;

        // シーンロード時のイベント登録（重複登録を防ぐため一度解除してから登録）
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        //シーン遷移しても壊れない
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// フェードアウト、暗くする
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public async UniTask FadeOut(float duration = _DEFAULT_FADE_DURATION) {
        await FadeTargetAlpha(1.0f, duration);
    }

    /// <summary>
    /// フェードイン、どうだ？明るくなっただろう？
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public async UniTask FadeIn(float duration = _DEFAULT_FADE_DURATION) {
        await FadeTargetAlpha(0.0f, duration);
    }

    /// <summary>
    /// フェード画像を指定の不透明度に変化させる
    /// </summary>
    /// <param name="targetAlpha"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private async UniTask FadeTargetAlpha(float targetAlpha, float duration) {
        float elapsedTime = 0.0f;//経過時間
        float startAlpha = _fadeImage.color.a;  //開始透明度
        Color targetColor = _fadeImage.color;
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            //保管した不透明度をフェード画像に設定
            float t = elapsedTime / duration;

            targetColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            _fadeImage.color = targetColor;
            //1フレーム待ち
            await UniTask.Delay(1);
        }
        targetColor.a = targetAlpha;
        _fadeImage.color = targetColor;
    }

    /// <summary>
    /// シーンが切り替わったときのための物
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //フェードイン
        _ = FadeIn();
    }
}
