
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenChester : MonoBehaviour {
    // イベント用の参照
    public static PlayerOpenChester instance;

    public delegate void InteractAction();
    // 宝箱などに通知するイベント
    public static event InteractAction OnInteract;
    


    private void Awake() {
        instance = this;
    }

    // Input Systemのイベントで呼ばれる
    public void HandleInteractInput(InputAction.CallbackContext context) {
            OnInteract?.Invoke();
    }

}
