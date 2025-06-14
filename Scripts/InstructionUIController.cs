using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionUIController : MonoBehaviour
{
    public GameObject instructionPanel;

    public void ShowInstructions()
    {
        instructionPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionPanel.SetActive(false);
    }
}
