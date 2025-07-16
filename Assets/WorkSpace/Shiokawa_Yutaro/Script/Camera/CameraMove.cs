using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    private PlayerCharacter player;
    private float angleX = 20f;
    private float rotateSpeed = 3f;     // �}�E�X���x

    private float angleY = 0f;         // ������]�p�x
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
    /// �J�����̎��_����
    /// </summary>
    private void ViewRoteto(Transform target)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        angleY += mouseX * rotateSpeed;
        angleX -= mouseY * rotateSpeed;
        angleX = Mathf.Clamp(angleX, -40f, 80f);

        // ��]
        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
        Vector3 desiredOffset = rotation * new Vector3(0, 0, -maxDistance);

        // �v���C���[�̎��_���S�i��F���⓪������j
        Vector3 targetCenter = target.position + Vector3.up * 0.5f;

        // ���C�L���X�g�ŏ�Q�������邩����
        RaycastHit hit;
        Vector3 finalOffset = desiredOffset;

        if (Physics.Raycast(targetCenter, desiredOffset.normalized, out hit, maxDistance))
        {
            // �Փ˓_�̏�����O�ɃJ������u���i0.2f�����O�ɏo���j
            float hitDist = Mathf.Clamp(hit.distance - 0.2f, minDistance, maxDistance);
            finalOffset = desiredOffset.normalized * hitDist;
        }

        // �J�����ʒu������
        transform.position = targetCenter + finalOffset;
        transform.LookAt(targetCenter);
    }

}
