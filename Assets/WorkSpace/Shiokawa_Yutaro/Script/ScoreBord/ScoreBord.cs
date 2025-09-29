using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBord : MonoBehaviour
{
    [SerializeField] Sprite[] rankingSprite;
    [SerializeField] Image rankingImage;


    [SerializeField] TextMeshProUGUI enemyDeadCountText;
    [SerializeField] TextMeshProUGUI timeText;
    public static int enemyDeadCount;
    public static float time;
    public PlayerCharacter player;

    float score;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OpenScore()
    {
        enemyDeadCountText.text = enemyDeadCount.ToString();
        timeText.text = time.ToString("0");

        score = enemyDeadCount * 10 + (int)(3000 / time);
        if (player.isDead) { score -= 200; }

        if (score < 200) { rankingImage.sprite = rankingSprite[0]; }
        if(score > 200) { rankingImage.sprite = rankingSprite[1]; }
        if(score > 300) { rankingImage.sprite = rankingSprite[2]; }
        if(score > 400) { rankingImage.sprite = rankingSprite[3]; }
    }

    static public void EnemyDead()
    {
        enemyDeadCount++;
    }
    static public void GameTime()
    {
        time += Time.deltaTime;
        //Debug.Log(time);
    }
}
