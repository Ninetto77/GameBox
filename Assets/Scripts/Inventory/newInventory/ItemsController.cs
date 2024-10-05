using InventorySystem;
using Items;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;

public class ItemsController : MonoBehaviour
{
	[Inject] private EquipmentManager equipmentManager;

	private ItemInfo item;
	private InventoryController inventory;
	private readonly string ActiveSlotName = GlobalStringsVars.ACTIVESLOT_NAME;

	private void Start()
	{
		inventory = InventoryController.instance;
	}

	private void Update()
	{
		string inventoryname = "";

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			inventoryname = InventoryType.GetInventoryName(ItemType.melle);
			SetActiveSlot(inventoryname);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			inventoryname = InventoryType.GetInventoryName(ItemType.lightWeapon);
			SetActiveSlot(inventoryname);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			inventoryname = InventoryType.GetInventoryName(ItemType.heavyWeapon);
			SetActiveSlot(inventoryname);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			inventoryname = InventoryType.GetInventoryName(ItemType.scientificWeapon);
			SetActiveSlot(inventoryname);
		}

	}

	private void SetActiveSlot(string inventoryname)
	{
		var item1 = inventory.GetInventory(inventoryname).InventoryGetItem(0).GetItemType();
		inventory.RemoveItemPos(ActiveSlotName, 0, 1);

		if (item1 != null)
		{
			inventory.AddItemPos(ActiveSlotName, item1, 0);
		}
	}

	/// <summary>
	/// Использование предмета
	/// </summary>
	public void UseItem()
	{
		item = inventory.GetItem(ActiveSlotName, 0).GetItemInfo();
		if (item != null)
		{
			item.Use(equipmentManager);
		}
	}

	/// <summary>
	/// Перестать использовать предмет
	/// </summary>
	public void StopUseItem()
	{
		try
		{
			equipmentManager.UnequiptAll();
		}
		catch { Debug.Log("no equipmentManager"); }
	}

	/// <summary>
	/// Бросить в мир объект
	/// </summary>
	/// <param name="pos"></param>
	/// <param name="item"></param>
	public void DropItem(Vector3 pos, InventoryItem item)
	{
		for (int i = 0; i < item.GetAmount(); i++)
		{
			Instantiate(item.GetRelatedGameObject(), pos, Quaternion.identity);
		}
	}

	/// <summary>
	/// Поменять местами объекты в инвентаре
	/// </summary>
	/// <param name="item1"></param>
	/// <param name="inSlot"></param>
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
