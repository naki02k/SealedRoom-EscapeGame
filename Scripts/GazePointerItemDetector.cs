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
    // トリガーに何かが入ったときに呼び出される
    private void OnTriggerEnter(Collider other)
    {
        // 入ってきたオブジェクトにInteractableItemコンポーネントがあるか確認
        InteractableItem item = other.GetComponent<InteractableItem>();
        if (item != null)
        {
            // 対象のアイテムを現在の注目アイテムとして保持
            currentItem = item;
            // マウスカーソルを合わせた際の処理（アウトライン表示など）を実行
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
        // 注目中のアイテムが存在し、左クリックされた場合
        if (currentItem != null && Input.GetMouseButtonDown(0))
        {
            // アイテムのクリック処理を呼び出す（アイテムを取得するなど）
            currentItem.OnClick();
            // 現在注目していたアイテムの情報をリセット
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
