using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BagOutlineTarget : MonoBehaviour
{
    public Outline outline;
    public string requiredItemID = "Key";
    public Animator animator;
    private bool isOpened = false;
    [SerializeField] private PocketManager pocketManager;
    void Awake()
    {
        if (outline == null) outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }
    public void OnHoverEnter()
    {
        if (HasRequiredItem() && !isOpened)
        {
            outline.enabled = true;
        }
    }
    public void OnHoverExit()
    {
        if (outline != null)
            outline.enabled = false;
    }
    public void OnClick()
    {
        if (HasRequiredItem() && !isOpened)
        {
            animator.SetTrigger("Open");
            isOpened = true;
            outline.enabled = false;
            if (pocketManager != null) pocketManager.RemoveItemByID(requiredItemID);
        }
    }
    private bool HasRequiredItem()
    {
        PocketManager pocket = FindObjectOfType<PocketManager>();
        return pocket != null && pocket.HasItem(requiredItemID);
    }
}
