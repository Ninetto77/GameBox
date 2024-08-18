using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public Button removeBtn;
    [Inject] Inventory inventory;
    [Inject] EquipmentManager equipmentManager;

	private ItemInfo item;

    /// <summary>
    /// ��������� ����
    /// </summary>
    /// <param name="newItem"></param>
    public void AddIcon(ItemInfo newItem)
    {
        item = newItem;

        icon.sprite = item.Icon;
        icon.enabled = true;
        removeBtn.interactable = true;
    }

    /// <summary>
    /// ������� ����
    /// </summary>
    public void ClearIcon()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeBtn.interactable = false;

    }

    /// <summary>
    /// ������� ��� ������� ������ �������� ����� ��������
    /// </summary>
    public void OnRemoveButton()
    {
        inventory.RemoveItem(item);
    }

    /// <summary>
    /// ������������� ��������
    /// </summary>
    public void UseItem()
    { 
		if (item != null)
        {
            item.Use(equipmentManager);
        }
    }
}
