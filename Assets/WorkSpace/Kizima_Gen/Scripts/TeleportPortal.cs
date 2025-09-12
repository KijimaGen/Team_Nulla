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


    void Start(){
        transform.position = new Vector3(transform.position.x,InitializePos,transform.position.z);
    }

    
    private async void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {

            await FadeManager.instance.FadeOut();
            SceneManager.LoadScene("Main");
        }
    }
}
