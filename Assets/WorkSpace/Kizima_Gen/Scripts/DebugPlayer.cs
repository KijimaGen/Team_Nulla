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
        // 移動方向を決定（X=横, Y=前後）
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // 物理的に移動
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
