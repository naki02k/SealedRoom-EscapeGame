using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public string itemId;
    void Start()
    {

        EnableOutline(false);
    }
    public string PickUp()
    {
        if (!ItemSaveDate.collectedItemIDs.Contains(itemId))
        {
            ItemSaveDate.collectedItemIDs.Add(itemId);
            Debug.Log($"�A�C�e��'{itemId}'���E���܂���");
        }
        Destroy(gameObject);
        Debug.Log($"Item ID:{itemId}��Ԃ��܂�");
        return itemId;
    }
    public void EnableOutline(bool enable)
    {
        var outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = enable;
    }
}
