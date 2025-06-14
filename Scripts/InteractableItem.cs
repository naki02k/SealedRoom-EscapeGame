using UnityEngine;
using UnityEngine.Events;

public class InteractableItem : MonoBehaviour
{
    private Outline outline;
    public ItemManager itemManager;
    private PocketManager pocketManager;
    void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
        pocketManager = FindObjectOfType<PocketManager>();
    }
    
    public void OnHoverEnter()
    {
        if (outline != null) outline.enabled = true;
    }
    // プレイヤーがアイテムに視線を合わせた（注目した）ときに呼ばれる処理
    public void OnHoverExit()
    {
        // アウトラインが設定されていれば表示を有効にする（アイテムを強調表示）
        if (outline != null) outline.enabled = false;
    }

    public void OnClick()
    {
        // アイテムマネージャーとポケットマネージャーが設定されているか確認
        if (itemManager != null && pocketManager != null)
        {
            // アイテムマネージャーからアイテムを取得し、そのIDを受け取る
            string pickedID = itemManager.PickUp();
            // 取得したIDをもとに、ポケットマネージャーにアイテムを追加
            pocketManager.AddItemByID(pickedID);
        }
        else
        {
            // どちらかのマネージャーが未設定の場合、エラーメッセージを出力
            Debug.Log("アイテムが存在しません");
        }
    }
}
