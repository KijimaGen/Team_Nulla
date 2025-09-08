using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalView : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portalOrigin;   // ワープ元ポータル
    public Transform portalExit;     // ワープ先ポータル
    public Camera portalCamera;

    void LateUpdate()
    {
        // プレイヤーとポータル元の相対位置を計算
        Vector3 offset = playerCamera.position - portalOrigin.position;
        portalCamera.transform.position = portalExit.position + offset;

        // 向きも同期
        Quaternion relativeRot = Quaternion.Inverse(portalOrigin.rotation) * playerCamera.rotation;
        portalCamera.transform.rotation = portalExit.rotation * relativeRot;
    }
}
