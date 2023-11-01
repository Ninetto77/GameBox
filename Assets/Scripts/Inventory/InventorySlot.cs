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
    /// ��������� ����
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
        Inventory.instance.RemoveItem(item);
    }

    /// <summary>
    /// ������������� ��������
    /// </summary>
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
