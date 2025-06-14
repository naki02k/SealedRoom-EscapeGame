using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public ColorButton[] colorButtons;
    public int[] correctIndexes;
    private bool isCleared = false;
    [SerializeField]
    private InteractionTargetController OutlineTarget;
    public DoorOutlineTarget doorOutlineTarget;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip accessGrantedSfx;//ê≥âÇÃéûÇÃâπ
    public void CheckPuzzle()
    {
        if (isCleared) return;
        for(int i = 0; i < colorButtons.Length; i++)
        {
            var button = colorButtons[i];
            int current = button.GetCurrentColorIndex();
            if (colorButtons[i].GetCurrentColorIndex() != correctIndexes[i])
                return;
        }
        isCleared = true;
        audioSource.PlayOneShot(accessGrantedSfx);
        Debug.Log("ê≥â");
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

