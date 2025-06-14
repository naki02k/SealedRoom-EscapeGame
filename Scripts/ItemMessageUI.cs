using UnityEngine;
using TMPro;
using System.Collections;

public class ItemMessageUI : MonoBehaviour
{
    public static ItemMessageUI Instance;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float displayTime = 2f;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        messageText.text = "";
    }

    public void ShowMessage(string message)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(DisplayMessage(message));
    }

    private IEnumerator DisplayMessage(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(displayTime);
        messageText.text = "";
    }
}
