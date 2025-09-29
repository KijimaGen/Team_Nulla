/*
 * @file TeleportPortal.cs
 * @brief テレポートポータル
 * @author kijima
 * @date 2025/9/8
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal : MonoBehaviour
{
    //初期位置
    const float InitializePos = 0.5f;
    //移動先
    public GameObject hole;
    [SerializeField] EnemyCharacter[] MiniBoss;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + InitializePos, transform.position.z);
    }


    private async void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            await FadeManager.instance.FadeOut();
            if (MiniBoss_OZN.MiniBossGame)
            {
                Destroy(collision.gameObject);
                MiniBoss_OZN.MiniBossGame = false;

                EnemyCharacter[] enemies = GameObject.FindObjectsOfType<EnemyCharacter>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    Destroy(enemies[i].gameObject);
                }

                Generator.instance.RunProgram();
                await FadeManager.instance.FadeIn(3);
                SceneManager.LoadScene("Main");
            }
            else
            {
                GameObject portal = Instantiate(hole, new Vector3(0, -7.4f, 4.8f), Quaternion.identity);
                //SceneManager.LoadScene("Main");
                collision.gameObject.transform.position = portal.transform.position;
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

                Destroy(portal);
                await FadeManager.instance.FadeIn(3);

                Instantiate(MiniBoss[0]);
                //
            }
            Destroy(this);

        }
    }
}
