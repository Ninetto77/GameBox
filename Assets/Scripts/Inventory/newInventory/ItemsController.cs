using InventorySystem;
using UnityEngine;
using Zenject;

public class ItemsController : MonoBehaviour
{
	[Inject] EquipmentManager equipmentManager;

	private ItemInfo item;
	private InventoryController inventory;
	private readonly string inventoryName = GlobalStringsVars.ACTIVESLOT_NAME;

	private void Start()
	{
		inventory = InventoryController.instance;
	}

	public void DropItem(Vector3 pos, InventoryItem item)
	{
		for (int i = 0; i < item.GetAmount(); i++)
		{
			Instantiate(item.GetRelatedGameObject(), pos, Quaternion.identity);
		}
	}
	/// <summary>
	/// Использование предмета
	/// </summary>
	public void UseItem()
	{
		item = inventory.GetItem(inventoryName, 0).GetItemInfo();
		if (item != null)
		{
			item.Use(equipmentManager);
		}
	}

	public void StopUseItem()
	{
		try
		{
			equipmentManager.UnequiptAll();
		}
		catch { Debug.Log("no equipmentManager"); }
	}

	public void Swap(InventoryItem item1, InventoryItem inSlot)
	{
		string item1inv = item1.GetInventory();
		string inSLotInv = inSlot.GetInventory();

		int positem1 = item1.GetPosition();
		int posinslotinv = inSlot.GetPosition();
		InventoryController.instance.RemoveItemPos(inSLotInv, inSlot.GetPosition(), inSlot.GetAmount());

		InventoryController.instance.AddItemPos(item1inv, inSlot.GetItemType(), positem1, inSlot.GetAmount());

		InventoryController.instance.AddItemPos(inSLotInv, item1.GetItemType(), posinslotinv, item1.GetAmount());
	}
}
