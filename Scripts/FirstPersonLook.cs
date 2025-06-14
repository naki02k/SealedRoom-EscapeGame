using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // �v���C���[�{�́i���E��]�p�j

    float xRotation = 0f;
    public bool lookEnabled = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �J�[�\�������b�N
    }

    void Update()
    {
        if (!lookEnabled) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // �㉺�̉�]����

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // �J�����㉺
        playerBody.Rotate(Vector3.up * mouseX); // �{�̍��E
    }
}
