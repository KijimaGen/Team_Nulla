using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour {
    [SerializeField]
    private Button Retry;
    [SerializeField]
    private Button Exit;
    private GameObject selectObject;
    // Start is called before the first frame update
    void Start()
    {
        Retry.Select();
    }

    // Update is called once per frame
    void Update()
    {
        selectObject = EventSystem.current.currentSelectedGameObject;
    }

    /// <summary>
    /// 決定
    /// </summary>
    public void Push(InputAction.CallbackContext context) {
        if (selectObject.name == "Retry") {
            SceneManager.LoadScene("Main");
        }
        else if (selectObject.name == "Exit") {
            Application.Quit();
        }
    }
    /// <summary>
    /// リトライボタン
    /// </summary>
    public void Up(InputAction.CallbackContext context) {
        Retry.Select();
    }
    /// <summary>
    /// ゲーム終了ボタン
    /// </summary>
    public void Down(InputAction.CallbackContext context) {
        Exit.Select();
    }

    public void Push()
    {
        if (selectObject.name == "Retry")
        {
            SceneManager.LoadScene("Main");
        }
        else if (selectObject.name == "Exit")
        {
            Application.Quit();
        }
    }
}
