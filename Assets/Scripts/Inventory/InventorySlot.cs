using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public Button removeBtn;

    private ItemInfo item;

    /// <summary>
    /// Добавляем слот
    /// </summary>
    /// <param name="newItem"></param>
    public void AddIcon(ItemInfo newItem)
    {
        item = newItem;

        icon.sprite = item.Sprite;
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
        Inventory.instance.RemoveItem(item);
    }

    /// <summary>
    /// Использование предмета
    /// </summary>
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
