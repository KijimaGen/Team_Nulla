/*
 * @file TeleportPortal.cs
 * @brief テレポートポータル
 * @author kijima
 * @date 2025/9/8
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal : MonoBehaviour{
    
    void Start(){
        
    }

    
    void Update(){
        
    }

    private async void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {

            await FadeManager.instance.FadeOut();
            SceneManager.LoadScene("Shiokawa");
        }
    }
}
