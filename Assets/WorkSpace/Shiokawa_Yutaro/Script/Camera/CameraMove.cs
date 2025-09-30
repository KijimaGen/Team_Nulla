using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    private float angleX = 20f;
    private float rotateSpeed = 3f;     // マウス感度

    private float angleY = 0f;         // 水平回転角度
    private float maxDistance = 1f;
    private float minDistance = 0.04f;

    Vector2 switchRStickValue;
    [SerializeField] Transform neck;
    [SerializeField] PlayerCharacter player;
    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeScale == 0f) return;
        //Vector3 playerPos = player.transform.position;
        ViewRoteto(player.transform);
        RotateNeck();
    }
    /// <summary>
    /// カメラの視点操作
    /// </summary>
    private void ViewRoteto(Transform target)
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        angleY += (switchRStickValue.x + mouseX) * rotateSpeed;
        angleX -= (switchRStickValue.y + mouseY) * rotateSpeed;
        angleX = Mathf.Clamp(angleX, -40f, 80f);

        // 回転
        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
        Vector3 desiredOffset = rotation * new Vector3(0, 0, -maxDistance);

        // プレイヤーの視点中心（例：肩や頭あたり）
        Vector3 targetCenter = target.position + Vector3.up * 0.5f;

        // レイキャストで障害物があるか判定
        RaycastHit hit;
        Vector3 finalOffset = desiredOffset;

        // 無視したいレイヤーを取得
        int ignoreLayer1 = LayerMask.NameToLayer("Item");
        int ignoreLayer2 = LayerMask.NameToLayer("Enemy");
        int ignoreLayer3 = LayerMask.NameToLayer("Player");

        // それらをまとめたマスクを作る
        int ignoreMask = (1 << ignoreLayer1) | (1 << ignoreLayer2) | (1 << ignoreLayer3);

        // 無視するためにビット反転
        int layerMask = ~ignoreMask;


        if (Physics.Raycast(targetCenter, desiredOffset.normalized, out hit, maxDistance, layerMask))
        {
            // 衝突点の少し手前にカメラを置く（0.2fだけ前に出す）
            float hitDist = Mathf.Clamp(hit.distance - 0.2f, minDistance, maxDistance);
            finalOffset = desiredOffset.normalized * hitDist;
        }

        // カメラ位置＆向き
        transform.position = targetCenter + finalOffset;
        transform.LookAt(targetCenter);
    }

    public void SwitchMove(InputAction.CallbackContext context)
    {
        switchRStickValue = context.ReadValue<Vector2>();

    }
    public void SwitchDontMove(InputAction.CallbackContext context)
    {
        switchRStickValue = Vector2.zero;

    }
    public void RotateNeck()
    {
        if (player.isAttacking) return;

        //// キャラの forward（体の正面）
        //Vector3 bodyForward = transform.forward;

        //// カメラ方向（水平成分だけ）
        //Vector3 camDir = Camera.main.transform.forward;
        //camDir.y = 0f;
        //camDir.Normalize();

        //// bodyForward と camDir の角度差
        //float angle = Vector3.SignedAngle(bodyForward, camDir, Vector3.up);

        //// 首の可動範囲（例：左右60°）
        //float maxAngle = 60f;
        //angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        //// 実際に向くべき方向を計算
        //Quaternion bodyRot = Quaternion.LookRotation(bodyForward);         // 体の回転
        //Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.up) * bodyRot; // 体基準で±angle回す

        //// 少し上方向も追従させたい場合
        //Vector3 lookTarget = neck.position + targetRot * Vector3.forward * 10f
        //                     + Camera.main.transform.up * 0.2f;

        //// 首をターゲット方向へスムーズに回す
        //Quaternion neckRot = Quaternion.LookRotation(lookTarget - neck.position);
        //neck.rotation = Quaternion.Slerp(neck.rotation, neckRot, Time.deltaTime * 5f);

        //neck.rotation = Quaternion.Lerp(neck.rotation,);
    }

}
