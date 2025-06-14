using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOutlineTarget : MonoBehaviour
{
    public Outline outline;
    public Animator doorAnimator;
    public bool isUnlocked = false;
    private bool isOpened = false;
    private void Awake()
    {
        if (outline == null) outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }
    public void OnHoverEnter()
    {
        if (isUnlocked && !isOpened)
            outline.enabled = true;
    }
    public void OnHoverExit()
    {
        outline.enabled = false;
    }
    public void OnClick()
    {
        if (isUnlocked && !isOpened)
        {
            doorAnimator.SetTrigger("Open");
            outline.enabled = false;
            isOpened = true;
        }
    }
    public void UnlockDoor()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
        isUnlocked = true;
    }
}
