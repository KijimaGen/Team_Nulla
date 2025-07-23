using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    private PlayerCharacter player;
    private float angleX = 20f;
    private float rotateSpeed = 3f;     // マウス感度

    private float angleY = 0f;         // 水平回転角度
    private float maxDistance = 1f;
    private float minDistance = 0.1f;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        ViewRoteto(player.transform);
    }
    /// <summary>
    /// カメラの視点操作
    /// </summary>
    private void ViewRoteto(Transform target)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        angleY += mouseX * rotateSpeed;
        angleX -= mouseY * rotateSpeed;
        angleX = Mathf.Clamp(angleX, -40f, 80f);

        // 回転
        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
        Vector3 desiredOffset = rotation * new Vector3(0, 0, -maxDistance);

        // プレイヤーの視点中心（例：肩や頭あたり）
        Vector3 targetCenter = target.position + Vector3.up * 0.5f;

        // レイキャストで障害物があるか判定
        RaycastHit hit;
        Vector3 finalOffset = desiredOffset;

        if (Physics.Raycast(targetCenter, desiredOffset.normalized, out hit, maxDistance))
        {
            // 衝突点の少し手前にカメラを置く（0.2fだけ前に出す）
            float hitDist = Mathf.Clamp(hit.distance - 0.2f, minDistance, maxDistance);
            finalOffset = desiredOffset.normalized * hitDist;
        }

        // カメラ位置＆向き
        transform.position = targetCenter + finalOffset;
        transform.LookAt(targetCenter);
    }

}
