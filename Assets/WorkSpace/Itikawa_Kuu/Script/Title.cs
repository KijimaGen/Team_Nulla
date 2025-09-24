using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Aƒ{ƒ^ƒ“
    /// </summary>
    public void Push(InputAction.CallbackContext context) {
        SceneManager.LoadScene("Main");
    }
}
