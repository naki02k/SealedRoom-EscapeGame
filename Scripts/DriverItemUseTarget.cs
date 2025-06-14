using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverItemUseTarget : MonoBehaviour
{
    public Outline outline;
    public string requiredItemID = "Driver";
    public PocketManager pocketManager;
    public GameObject deleteObject;
    public GameObject spawnItem;
    public Transform spawnPoint;
    public Vector3 spawnscale =new Vector3(0.1f, 0.01f, 0.1f);
    public GameObject fadeCanvasprefab;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private bool hasused = false;
    private void Awake()
    {
        if (outline == null) outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }
    public void OnHoverEnter()
    {
        if (!hasused && HasRequiredItem()) outline.enabled = true;
    }
    public void OnHoverExit()
    {
        if (outline != null) outline.enabled = false;
    }
    public void OnClick()
    {
        if (hasused || !HasRequiredItem()) return;
        StartCoroutine(UseDriverSequence());
    }
    private bool HasRequiredItem()
    {
        return pocketManager != null && pocketManager.HasItem(requiredItemID);
    }
    private IEnumerator UseDriverSequence()
    {
        hasused = true;
        outline.enabled = false;
        if (fadeCanvasprefab != null)
        {
            GameObject fadeInstance = Instantiate(fadeCanvasprefab);
            Animator fadeAnimator=fadeInstance.GetComponentInChildren<Animator>();
            if (fadeAnimator != null)
            {
                fadeAnimator.SetTrigger("FadeOut");
                yield return new WaitForSeconds(1f);
            }
            if (audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
                yield return new WaitForSeconds(audioClip.length);
            }
            if (deleteObject != null)
                deleteObject.SetActive(false);
            if (spawnItem != null && spawnPoint != null)
            {
                GameObject item = Instantiate(spawnItem, spawnPoint.position, spawnPoint.rotation);
                item.transform.localScale = spawnscale;
            }
            if (pocketManager != null)
                pocketManager.RemoveItemByID(requiredItemID);
            fadeAnimator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(1f);
        }
    }
}
