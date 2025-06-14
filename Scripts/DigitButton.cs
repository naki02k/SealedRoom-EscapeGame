using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitButton : MonoBehaviour
{
    public TextMeshPro digitText;
    private int currentDigit = 0;
    public CodeChecker codeChecker;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickedSfx;
    private void OnMouseDown()
    {
        currentDigit = (currentDigit + 1) % 10;
        UpdateDisplay();
        if (audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
        }
        if (codeChecker != null)
        {
            codeChecker.OnDigitChanged();
        }

    }
    void UpdateDisplay()
    {
        digitText.text = currentDigit.ToString();
    }
    public int GetDigit()
    {
        return currentDigit;
    }
}
