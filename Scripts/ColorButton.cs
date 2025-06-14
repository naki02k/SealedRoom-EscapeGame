using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    private Renderer rend;
    private int ColorIndex = 0;
    public Color[] colors;
    public PuzzleManager puzzleManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickedSfx;
    private void Start()
    {
        ColorIndex = 0;
        rend = GetComponent<Renderer>();
        UpdateColor();
    }
    private void OnMouseDown()
    {
        ColorIndex = (ColorIndex + 1) % colors.Length;
        UpdateColor();
        if (audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
        }
        puzzleManager.CheckPuzzle();
    }
    private void UpdateColor()
    {
        if (rend != null && colors.Length > 0)
        {
            rend.material.color = colors[ColorIndex];
        }
    }
    public int GetCurrentColorIndex()
    {
        return ColorIndex;
    }
}
