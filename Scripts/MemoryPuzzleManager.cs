using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPuzzleManager : MonoBehaviour
{
    public List<string> correctSequence = new List<string> { "Red", "Blue", "Red" };
    private List<string> playerInput = new List<string>();
    private bool acceptingInput = true;
    public float resetDelay = 3f;
    private float inputTimer = 0f;
    public float maxWaitTime = 5f; // 何秒も入力がなければリセット
    [SerializeField] private InteractionTargetController OutlineTarget;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip accessGrantedSfx;//正解の時の音
    private GameObject spawnedMemo;
    [Header("Door Control")]
    [SerializeField] private Transform doorObject;
    [SerializeField] private DoorOutlineTarget doorOutlineTarget;
    private void Update()
    {
        if (playerInput.Count > 0 && acceptingInput)
        {
            inputTimer += Time.deltaTime;
            if (inputTimer >= maxWaitTime)
            {
                Debug.Log("時間切れでリセット");
                ResetPuzzle();
            }
        }
    }

    public void OnButtonPressed(string color)
    {
        if (!acceptingInput) return;

        playerInput.Add(color);
        inputTimer = 0f; // 入力があったのでタイマーリセット

        int index = playerInput.Count - 1;
        if (playerInput[index] != correctSequence[index])
        {
            Debug.Log("不正解！");
            acceptingInput = false;
            Invoke(nameof(ResetPuzzle), resetDelay);
            return;
        }

        if (playerInput.Count == correctSequence.Count)
        {
            Debug.Log("正解！");
            acceptingInput = false;
            audioSource.PlayOneShot(accessGrantedSfx);
            //SpawnMemo();
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
    }
    public void ResetPuzzle()
    {
        playerInput.Clear();
        acceptingInput = true;
        inputTimer = 0f;
        Debug.Log("リセットされました");
    }
    public GameObject GetSpawnMemo()
    {
        return spawnedMemo;
    }
}
