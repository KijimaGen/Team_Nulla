/*
 * @file TeleportPortal.cs
 * @brief テレポートポータル
 * @author kijima
 * @date 2025/9/8
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal : MonoBehaviour{
    //初期位置
    const float InitializePos = 0.5f;
    //移動先
    private GameObject hole;
    [SerializeField] EnemyCharacter[] MiniBoss;

    void Start(){
        hole = GameObject.Find("PortalHole");
        transform.position = new Vector3(transform.position.x,InitializePos,transform.position.z);
    }

    
    private async void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {

            await FadeManager.instance.FadeOut();

           // SceneManager.LoadScene("Main");
           collision.gameObject.transform.position = hole.transform.position;
           collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            await FadeManager.instance.FadeIn(3);

            Instantiate(MiniBoss[0]);
            hole.SetActive(false);
        }
    }
}
