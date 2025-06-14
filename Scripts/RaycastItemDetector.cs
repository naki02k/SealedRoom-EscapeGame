using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItemDetector : MonoBehaviour
{
    public float detectDistance = 2.0f;
    private Camera cam;

    private InteractableItem currentItem;
    private InteractionTargetController currentPuzzle;
    private DoorOutlineTarget currentDoor;
    private BagOutlineTarget currentBag;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        // ��x�A�E�g���C����S�ď���
        ClearHighlights();

        if (Physics.Raycast(ray, out hit, detectDistance))
        {
            bool interacted = false;

            // �A�C�e��
            InteractableItem item = hit.collider.GetComponent<InteractableItem>();
            if (item != null)
            {
                currentItem = item;
                currentItem.OnHoverEnter();
                interacted = true;

                if (Input.GetMouseButtonDown(0))
                {
                    currentItem.OnClick();
                }
            }

            // �p�Y��
            var puzzle = hit.collider.GetComponent<InteractionTargetController>();
            if (puzzle != null)
            {
                currentPuzzle = puzzle;
                currentPuzzle.OnHoverEnter();
                interacted = true;

                if (Input.GetMouseButtonDown(0))
                {
                    currentPuzzle.OnClick();
                }
            }

            // �h�A
            var door = hit.collider.GetComponent<DoorOutlineTarget>();
            if (door != null)
            {
                currentDoor = door;
                currentDoor.OnHoverEnter();
                interacted = true;

                if (Input.GetMouseButtonDown(0))
                {
                    currentDoor.OnClick();
                }
            }

            // �J�o��
            var bag = hit.collider.GetComponent<BagOutlineTarget>();
            if (bag != null)
            {
                currentBag = bag;
                currentBag.OnHoverEnter();
                interacted = true;

                if (Input.GetMouseButtonDown(0))
                {
                    currentBag.OnClick();
                }
            }

            if (!interacted)
            {
                ClearHighlights();
            }
        }
        else
        {
            ClearHighlights();
        }
    }

    void ClearHighlights()
    {
        if (currentItem != null)
        {
            currentItem.OnHoverExit();
            currentItem = null;
        }

        if (currentPuzzle != null)
        {
            currentPuzzle.OnHoverExit();
            currentPuzzle = null;
        }

        if (currentDoor != null)
        {
            currentDoor.OnHoverExit();
            currentDoor = null;
        }

        if (currentBag != null)
        {
            currentBag.OnHoverExit();
            currentBag = null;
        }
    }
}
