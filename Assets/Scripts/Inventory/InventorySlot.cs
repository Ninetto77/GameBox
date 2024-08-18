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
    /// Добавляем слот
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
    /// Очищаем слот
    /// </summary>
    public void ClearIcon()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeBtn.interactable = false;

    }

    /// <summary>
    /// Событие при нажатии кнопки удаления слота предмета
    /// </summary>
    public void OnRemoveButton()
    {
        inventory.RemoveItem(item);
    }

    /// <summary>
    /// Использование предмета
    /// </summary>
    public void UseItem()
    { 
		if (item != null)
        {
            item.Use(equipmentManager);
        }
    }
}
