using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryButton : MonoBehaviour
{
    public string buttonColor;
    public MemoryPuzzleManager memoryPuzzle;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickedSfx;
    private void OnMouseDown()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
        }
        memoryPuzzle.OnButtonPressed(buttonColor);
    }
}
