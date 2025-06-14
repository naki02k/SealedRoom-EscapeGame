using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PocketManager : MonoBehaviour
{
    public List<PocketItem> pocketItems = new List<PocketItem>();
    public ItemData itemDatabase;
    public GameObject pocketUI;
    public GameObject itemPreviewRoom;
    public Transform itemSpawnPoint;
    public Camera itemUICamera;
    public RawImage itemPreviewImage;
    public TextMeshProUGUI itemNameText;
    [SerializeField] FirstPersonLook personLook;
    [SerializeField] FirstPersonMovement movement;
    private GameObject currentItemPreview;
    private bool isPocketVisible = false;
    private int currentIndex = 0;
    public Camera mainCamera;
    public Camera puzzleCamera;
    void Start()
    {
        foreach(string id in ItemSaveDate.collectedItemIDs)
        {
            PocketItem item = itemDatabase.itemList.Find(i => i.id == id);
            if (item != null && !pocketItems.Contains(item))
            {
                pocketItems.Add(item);
            }
        }
        pocketUI.SetActive(isPocketVisible);
        itemPreviewRoom.SetActive(isPocketVisible);
        itemUICamera.gameObject.SetActive(isPocketVisible);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePocketDisplay();
        }
    }
    public void AddItemByID(string id)
    {

        //データベースから指定IDに一致するアイテムを探す
        PocketItem item = itemDatabase.itemList.Find(i => i.id == id);
        if (item != null)
        {
            // 見つかったアイテムをプレイヤーのポケットに追加
            pocketItems.Add(new PocketItem
            {
                id = item.id,
                item = item.item,
                explainText = item.explainText
            });
            // デバッグログで追加を確認
            Debug.Log($"ポケットに'{item.id}'を追加しました");
        }
        else
        {
            // IDが存在しない場合は警告を出す
            Debug.LogWarning($"アイテム ID'{id}'はデータベースに存在しません");
        }
    }
    void TogglePocketDisplay()
    {
        if (puzzleCamera.gameObject.activeSelf)
        {
            Debug.Log("パズル中のためポケットを開けません");
            return;
        }
        if (!isPocketVisible && pocketItems.Count == 0)
        {
            Debug.Log("ポケットが空なので表示しません。");
            return;
        }
        isPocketVisible = !isPocketVisible;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        personLook.enabled = false;
        movement.enabled = false;
        pocketUI.SetActive(isPocketVisible);
        itemPreviewRoom.SetActive(isPocketVisible);
        itemUICamera.gameObject.SetActive(isPocketVisible);

        if (isPocketVisible && pocketItems.Count > 0)
        {
            currentIndex = 0;
            ShowItemPreview(pocketItems[currentIndex]);
        }
        else
        {
            ClearPreview();
        }
    }

    void ShowItemPreview(PocketItem pocketItem)
    {
        if (currentItemPreview != null)
        {
            Destroy(currentItemPreview);
        }

        if (pocketItem.item == null) return;

        currentItemPreview = Instantiate(
            pocketItem.item,
            itemSpawnPoint.position,
            Quaternion.identity,
            itemPreviewRoom.transform
        );
        currentItemPreview.transform.rotation = Quaternion.Euler(90f, 180f, 0f);

        SetLayerRecursively(currentItemPreview.transform, LayerMask.NameToLayer("PocketItem"));

        itemNameText.text = pocketItem.explainText; // ← 名前をUIに表示
    }

    void ClearPreview()
    {
        if (currentItemPreview != null)
        {
            Destroy(currentItemPreview);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        personLook.enabled = true;
        movement.enabled = true;
        itemNameText.text = "";
    }
    
    void SetLayerRecursively(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        foreach (Transform child in obj)
        {
            SetLayerRecursively(child, layer);
        }
    }
    public void ShowNextItem()
    {
        if (pocketItems.Count == 0) return;
        if (currentIndex < pocketItems.Count - 1)
        {
            currentIndex++;
            ShowItemPreview(pocketItems[currentIndex]);
        }
    }
    public void ShowPreviousItem()
    {
        if (pocketItems.Count == 0) return;
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowItemPreview(pocketItems[currentIndex]);
        }
    }
    public bool HasItem(string id)
    {
        return pocketItems.Exists(item => item.id == id);
    }
    public void RemoveItemByID(string id)
    {
        PocketItem itemToRemove = pocketItems.Find(item => item.id == id);
        if (itemToRemove != null)
        {
            pocketItems.Remove(itemToRemove);
            Debug.Log($"ポケットから'{id}'を削除しました");
            if (pocketItems.Count > 0)
            {
                currentIndex = Mathf.Clamp(currentIndex, 0, pocketItems.Count - 1);
                ShowItemPreview(pocketItems[currentIndex]);
            }
            else
            {
                ClearPreview();
            }
        }
        else
        {
            Debug.Log($"ポケットに'{id}'は見つかりませんでした");
        }
    }
}
