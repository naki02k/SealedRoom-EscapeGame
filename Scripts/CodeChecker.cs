using System.Collections;
using UnityEngine;

public class CodeChecker : MonoBehaviour
{
    public DigitButton[] digitButtons;
    public int[] correctCode = new int[4] { 0, 7, 1, 2 };
    public GameObject keyPrefab;
    public Transform spawnPoint;
    public float delayBeforeCheck = 1.5f; // 判定までの待機時間
    public Transform doorObject;
    [SerializeField] private InteractionTargetController OutlineTarget;
    public DoorOutlineTarget doorOutlineTarget;

    private bool isSolved = false;
    private Coroutine checkCoroutine;
    private GameObject spawnKey;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip accessGrantedSfx;//正解の時の音
    public void OnDigitChanged()
    {
        if (isSolved) return;

        // すでにチェック中なら一旦キャンセルしてやり直す
        if (checkCoroutine != null)
        {
            StopCoroutine(checkCoroutine);
        }

        // 新しくカウントダウンを開始
        checkCoroutine = StartCoroutine(DelayedCheck());
    }

    private IEnumerator DelayedCheck()
    {
        yield return new WaitForSeconds(delayBeforeCheck);

        // コードが正しいか確認
        for (int i = 0; i < digitButtons.Length; i++)
        {
            if (digitButtons[i].GetDigit() != correctCode[i])
            {
                Debug.Log("不正解");
                yield break;
            }
        }

        Debug.Log("正解");
        isSolved = true;
        audioSource.PlayOneShot(accessGrantedSfx);
        SpawnKey();
        if (OutlineTarget != null)
        {
            OutlineTarget.outline.enabled = false;
            OutlineTarget.isSolved = true;
            OutlineTarget.ReturnToMainView();
        }

        if (doorOutlineTarget != null)
        {
            doorOutlineTarget.UnlockDoor();
        }
    }
    
    public void SpawnKey()
    {
        if (keyPrefab == null || spawnPoint == null) return;
        spawnKey = Instantiate(keyPrefab, spawnPoint.position, spawnPoint.rotation);
        if (spawnKey.name.Contains("Memo_2"))
        {
            spawnKey.transform.localScale = new Vector3(0.1f, 0.01f, 0.1f); 
        }
        else
        {
            spawnKey.transform.localScale = new Vector3(2f, 2f, 2f); 
        }
        if (doorObject != null)
        {
            spawnKey.transform.SetParent(doorObject);
        }
        InteractableItem keyItem = spawnKey.GetComponent<InteractableItem>();
        if (keyItem != null)
        {
            keyItem.enabled = false;
        }
        Outline outline = spawnKey.GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }
    public GameObject GetSpawnKey()
    {
        return spawnKey;
    }
}
