using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // プレイヤー本体（左右回転用）

    float xRotation = 0f;
    public bool lookEnabled = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
    }

    void Update()
    {
        if (!lookEnabled) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 上下の回転制限

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // カメラ上下
        playerBody.Rotate(Vector3.up * mouseX); // 本体左右
    }
}
