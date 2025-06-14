using UnityEngine;
using System.Collections;

public class PlayerWakeUp : MonoBehaviour
{
    public Camera playerCamera;
    public float wakeUpDuration = 3f;
    public float standUpDuration = 1.5f;
    public Vector3 standingCameraLocalPos = new Vector3(0, 0.75f, 0);
    public Vector3 lyingCameraLocalPos = new Vector3(0, 0.5f, 0);

    private bool isWakingUp = true;
    private bool cursorLooked = false;
    private CharacterController controller;
    private FirstPersonLook lookScript;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lookScript = playerCamera.GetComponent<FirstPersonLook>();
        playerCamera.transform.localPosition = lyingCameraLocalPos;
        playerCamera.transform.localRotation = Quaternion.Euler(90, 0, 0);
        lookScript.enabled = false;
        controller.enabled = false;
        StartCoroutine(WakeUpRoutine());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    IEnumerator WakeUpRoutine()
    {
        // STEP1�F�O������
        float elapsed = 0f;
        Quaternion startRot = playerCamera.transform.localRotation;
        Quaternion endRot = Quaternion.Euler(0, 0, 0);

        while (elapsed < wakeUpDuration)
        {
            float t = elapsed / wakeUpDuration;
            playerCamera.transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.localRotation = endRot;

        // STEP2�F�����ō��E������
        yield return StartCoroutine(AutoLookAround());

        // STEP3�F�����オ��
        elapsed = 0f;
        Vector3 startPos = lyingCameraLocalPos;
        Vector3 endPos = standingCameraLocalPos;

        while (elapsed < standUpDuration)
        {
            float t = elapsed / standUpDuration;
            playerCamera.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.localPosition = endPos;

        // STEP4�F����\�ɂ���
        lookScript.enabled = true;
        controller.enabled = true;
        isWakingUp = false;
    }

    IEnumerator AutoLookAround()
    {
        float duration = 2.5f;
        float elapsed = 0f;
        float angleRange = 30f; // ���E30�x
        float speed = 2f;

        while (elapsed < duration)
        {
            float yaw = Mathf.Sin(elapsed * speed) * angleRange;
            Vector3 angles = playerCamera.transform.localEulerAngles;
            angles.y = yaw;
            playerCamera.transform.localEulerAngles = angles;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // �Ō�ɒ����ɖ߂��i�Ȃ߂炩�Ɂj
        float returnDuration = 0.5f;
        float returnElapsed = 0f;
        float startYaw = playerCamera.transform.localEulerAngles.y;
        float targetYaw = 0f;

        while (returnElapsed < returnDuration)
        {
            float t = returnElapsed / returnDuration;
            float currentYaw = Mathf.LerpAngle(startYaw, targetYaw, t);

            Vector3 angles = playerCamera.transform.localEulerAngles;
            angles.y = currentYaw;
            playerCamera.transform.localEulerAngles = angles;

            returnElapsed += Time.deltaTime;
            yield return null;
        }

        // �ŏI�p�x���s�^�b�ƍ��킹��
        Vector3 finalAngles = playerCamera.transform.localEulerAngles;
        finalAngles.y = 0f;
        playerCamera.transform.localEulerAngles = finalAngles;

    }
}
