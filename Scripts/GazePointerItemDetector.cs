using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GazePointerItemDetector : MonoBehaviour
{
    private InteractableItem currentItem;
    private InteractionTargetController currentPazle;
    private DoorOutlineTarget currentDoor;
    private BagOutlineTarget currentBag;
    private DriverItemUseTarget currentTarget;
    public float showDistance = 2f;
    private Transform cameraTransform;
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    // �g���K�[�ɉ������������Ƃ��ɌĂяo�����
    private void OnTriggerEnter(Collider other)
    {
        // �����Ă����I�u�W�F�N�g��InteractableItem�R���|�[�l���g�����邩�m�F
        InteractableItem item = other.GetComponent<InteractableItem>();
        if (item != null)
        {
            // �Ώۂ̃A�C�e�������݂̒��ڃA�C�e���Ƃ��ĕێ�
            currentItem = item;
            // �}�E�X�J�[�\�������킹���ۂ̏����i�A�E�g���C���\���Ȃǁj�����s
            currentItem.OnHoverEnter();
        }
        InteractionTargetController keypad = other.GetComponent<InteractionTargetController>();
        if (keypad != null)
        {
            currentPazle = keypad;
            currentPazle.OnHoverEnter();
            return;
        }
        DoorOutlineTarget door = other.GetComponent<DoorOutlineTarget>();
        if (door != null)
        {
            currentDoor = door;
            currentDoor.OnHoverEnter();
            return;
        }
        BagOutlineTarget bag = other.GetComponent<BagOutlineTarget>();
        if (bag != null)
        {
            currentBag = bag;
            currentBag.OnHoverEnter();
            return;
        }
        DriverItemUseTarget driver = other.GetComponent<DriverItemUseTarget>();
        if (driver != null)
        {
            currentTarget = driver;
            currentTarget.OnHoverEnter();
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableItem item = other.GetComponent<InteractableItem>();
        if (item != null && item == currentItem)
        {
            currentItem.OnHoverExit();
            currentItem = null;
        }
        InteractionTargetController keypad = other.GetComponent<InteractionTargetController>();
        if (keypad != null&&keypad==currentPazle)
        {
            currentPazle.OnHoverExit();
            currentPazle = null;
        }
        DoorOutlineTarget door = other.GetComponent<DoorOutlineTarget>();
        if (door != null && door == currentDoor)
        {
            currentDoor.OnHoverExit();
            currentDoor = null;
        }
        BagOutlineTarget bag = other.GetComponent<BagOutlineTarget>();
        if (bag != null&&bag==currentBag)
        {
            currentBag.OnHoverExit();
            currentBag = null;
        }
        DriverItemUseTarget driver = other.GetComponent<DriverItemUseTarget>();
        if (driver != null&&driver==currentTarget)
        {
            currentTarget.OnHoverExit();
            currentTarget = null;
        }
    }

    void Update()
    {
        // ���ڒ��̃A�C�e�������݂��A���N���b�N���ꂽ�ꍇ
        if (currentItem != null && Input.GetMouseButtonDown(0))
        {
            // �A�C�e���̃N���b�N�������Ăяo���i�A�C�e�����擾����Ȃǁj
            currentItem.OnClick();
            // ���ݒ��ڂ��Ă����A�C�e���̏������Z�b�g
            currentItem = null;
        }
        if (currentPazle != null && Input.GetMouseButtonDown(0))
        {
            currentPazle.OnClick();
            currentPazle = null;
        }
        if (currentDoor != null && Input.GetMouseButtonDown(0))
        {
            currentDoor.OnClick();
            currentDoor = null;
        }
        if (currentBag != null && Input.GetMouseButtonDown(0))
        {
            currentBag.OnClick();
            currentBag = null;
        }
        if (currentTarget != null && Input.GetMouseButtonDown(0))
        {
            currentTarget.OnClick();
            currentTarget = null;
        }
    }
}
