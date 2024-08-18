using UnityEngine;
using Zenject;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    private InventorySlot[] slots;
	[Inject] private Inventory inventory;
    void Start()
    {
        //�������
        inventory.OnItemChangedCallback += UpdateUI;

        //�������� ������ ������
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    /// <summary>
    /// ���������� UI ���������
    /// </summary>
    void UpdateUI()
    {
        for (int i=0; i < slots.Length; i++)
        {
            if (i <  inventory.items.Count)
            {
                slots[i].AddIcon(inventory.items[i]);
            }
            else
            {
                slots[i].ClearIcon();
            }
        }
    }
}
