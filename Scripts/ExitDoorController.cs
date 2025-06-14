using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExitDoorController : MonoBehaviour
{
    public GameObject fadeCanvasWhite;
    public GameObject gameClearCanvas;
    public TextMeshProUGUI clearTimeText;
    public FirstPersonMovement movement;
    public FirstPersonLook personLook;
    private GameObject fadeInstance;
    private GameObject gameClearInstance;
    public void OpenDoorAndExit()
    {
        StartCoroutine(FadeAndShowGameClear());
    }
    private IEnumerator FadeAndShowGameClear()
    {
        movement.enabled = false;
        personLook.enabled = false;
        fadeInstance = Instantiate(fadeCanvasWhite);
        Animator animator = fadeInstance.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("FadeOut_White");
        }
        yield return new WaitForSeconds(2f);
        Debug.Log($"2秒後のTime.time:{Time.time}");
        GameManager.Instance.StopTime();
        float elapsed = GameManager.Instance.StopTime();
        Debug.Log($"ゲームクリア時間（取得）: {elapsed}");
        gameClearCanvas.SetActive(true);
        if (clearTimeText != null)
        {
            clearTimeText.text = $"クリアタイム:{elapsed:F1}秒";
        }
    }
}
