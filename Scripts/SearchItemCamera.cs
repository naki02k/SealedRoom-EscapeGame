using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchItemCamera : MonoBehaviour
{
    public PocketManager pocketManager;

    private ItemManager currentItem;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            ItemManager itemManager = hit.collider.GetComponent<ItemManager>();

            if (itemManager != null)
            {
                if (currentItem != itemManager)
                {
                    if (currentItem != null) currentItem.EnableOutline(false);
                    currentItem = itemManager;
                    currentItem.EnableOutline(true);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    string pickedID = currentItem.PickUp();
                    pocketManager.AddItemByID(pickedID);
                    currentItem = null;
                }
            }
        }
        else
        {
            if (currentItem != null)
            {
                currentItem.EnableOutline(false);
                currentItem = null;
            }
        }
    }
}
