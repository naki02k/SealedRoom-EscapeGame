using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPreviewManager : MonoBehaviour
{
    public GameObject ItemPreviewRoom;
    public Transform itemSpawnPoint;
    public Camera ItemUICamera;
    public RenderTexture itemRenderTexture;

    private GameObject currentItem;
    private bool isPreviewActive = false;
   
    public void ShowItemPreview(GameObject itemPrefab)
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
        }
        if (itemPrefab == null)
        {
            return;
        }
        currentItem = Instantiate(itemPrefab, itemSpawnPoint.position, Quaternion.identity, ItemPreviewRoom.transform);
        SetLayerRecursively(currentItem.transform, LayerMask.NameToLayer("PocketItem"));
    }
    
    void SetLayerRecursively(Transform obj,int layer)
    {
        obj.gameObject.layer = layer;
        foreach(Transform child in obj)
        {
            SetLayerRecursively(child, layer);
        }
    }
}
