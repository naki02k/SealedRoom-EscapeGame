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
    // �v���C���[���A�C�e���Ɏ��������킹���i���ڂ����j�Ƃ��ɌĂ΂�鏈��
    public void OnHoverExit()
    {
        // �A�E�g���C�����ݒ肳��Ă���Ε\����L���ɂ���i�A�C�e���������\���j
        if (outline != null) outline.enabled = false;
    }

    public void OnClick()
    {
        // �A�C�e���}�l�[�W���[�ƃ|�P�b�g�}�l�[�W���[���ݒ肳��Ă��邩�m�F
        if (itemManager != null && pocketManager != null)
        {
            // �A�C�e���}�l�[�W���[����A�C�e�����擾���A����ID���󂯎��
            string pickedID = itemManager.PickUp();
            // �擾����ID�����ƂɁA�|�P�b�g�}�l�[�W���[�ɃA�C�e����ǉ�
            pocketManager.AddItemByID(pickedID);
        }
        else
        {
            // �ǂ��炩�̃}�l�[�W���[�����ݒ�̏ꍇ�A�G���[���b�Z�[�W���o��
            Debug.Log("�A�C�e�������݂��܂���");
        }
    }
}
