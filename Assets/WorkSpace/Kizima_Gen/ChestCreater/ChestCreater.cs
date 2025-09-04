/*
 * @file ChestCreater.cs
 * @brief チェストを作るよん
 * @author kijima
 * @date 2025/9/3
 */
using System.Linq;
using TMPro;
using UnityEngine;

public class ChestCreater : MonoBehaviour{
    //呼び出すチェスト
    [SerializeField]
    private Chest chest;
    //残りチェスト数
    private int chestCount;
    //残りチェスト数表示UI
    [SerializeField]
    private TextMeshProUGUI chestCountText;

    const string text = "nokori : ";


    void Start(){
        for (int i = 0; i < 100; i++) {
            Vector3 chestSpawnPos = new Vector3(Random.Range(17, 87), 5, (Random.Range(14, 87)));
            Instantiate(chest,chestSpawnPos,Quaternion.identity,transform);
        }
    }

    
    void Update(){
        chestCount = this.transform.childCount-1;

        chestCountText.text = text + chestCount;
    }
}
