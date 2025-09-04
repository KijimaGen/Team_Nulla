/*
 * @file DebugPlayer.cs
 * @brief デバッグ用のプレイヤームーバー
 * @author Sum1r3
 * @date 2025/9/4
 */
using UnityEngine;
using UnityEngine.InputSystem; // ← 新InputSystemを使うとき必要


public class DebugPlayer : MonoBehaviour {

    //物理、入力量
    private Rigidbody rb;
    private Vector2 moveInput;

    //移動スピード
    [SerializeField] private float moveSpeed = 5f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    // Input System の "Move" イベントから呼ばれる
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        // Y成分を0にして地面に沿わせる
        camForward.y = 0;
        camRight.y = 0;

        // カメラ基準で移動
        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        rb.MovePosition(rb.position + move.normalized * moveSpeed * Time.fixedDeltaTime);
        if (transform.position.y < -0.5) {
            transform.position = new Vector3(0,1,0);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
