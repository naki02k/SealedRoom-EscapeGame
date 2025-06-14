using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingPuzzle : MonoBehaviour
{
    public StretchCube[] cubes;
    public float[] targetHeghts = { 2, 0, 1, 3 };
    [SerializeField] private InteractionTargetController OutlineTarget;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip accessGrantedSfx;//ê≥âÇÃéûÇÃâπ
    [Header("Door Control")]
    [SerializeField] private Transform doorObject;
    [SerializeField] private DoorOutlineTarget doorOutlineTarget;
    private bool isAlreadySolved = false;
    void Update()
    {
        if (isAlreadySolved) return;
        if (IsCorrect())
        {
            Debug.Log("ê≥â");
            isAlreadySolved = true;
            audioSource.PlayOneShot(accessGrantedSfx);
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
    bool IsCorrect()
    {
        for(int i = 0; i < cubes.Length; i++)
        {
            float currentY = cubes[i].cube.position.y;
            if (Mathf.RoundToInt(currentY) != Mathf.RoundToInt(targetHeghts[i]))
            {
                return false;
            }
        }
        return true;
    }
}
