using Cache;
using InventorySystem;
using Items;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class ItemsController : MonoCache
{
	[Inject] private EquipmentManager equipmentManager;

	private ItemInfo item;
	private InventoryController inventory;
	private readonly string ActiveSlotName = GlobalStringsVars.ACTIVESLOT_NAME;

	private void Start()
	{
		inventory = InventoryController.instance;
	}

	public override void OnTick()
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
		var itemInInventory = inventory.GetInventory(inventoryname).InventoryGetItem(0).GetItemType();
		CleanActiveSlot();

		if (itemInInventory != null)
		{
			inventory.AddItemPos(ActiveSlotName, itemInInventory, 0);
		}
	}

	private void CleanActiveSlot()
	{
		inventory.RemoveItemPos(ActiveSlotName, 0, 1);
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
	public void DropItem(InventoryItem item)
	{
		for (int i = 0; i < item.GetAmount(); i++)
		{
			var obj = equipmentManager.GetPlayerHand();
			if (obj == null)
				return;

			ItemPickup dropedItem = obj.gameObject.GetComponent<ItemPickup>();

			if (dropedItem != null)
			{
				dropedItem.IsPicked = false;
				dropedItem.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		equipmentManager.DropHand();
	}


	public bool TryAddToInventory(ItemInfo item)
	{
		var type = item.ItemType;
		string inventoryname = InventoryType.GetInventoryName(type);

		bool isPicked = TryAdd(inventoryname, item);
		CleanActiveSlot();

		return isPicked;
	}


	/// <summary>
	/// Добавить предмет в инвентарь
	/// </summary>
	/// <param name="inventoryname"></param>
	/// <param name="item"></param>
	/// <returns></returns>
	private bool TryAdd(string inventoryname, ItemInfo item)
	{
		if (!inventory.InventoryFull(inventoryname, item.Name))
			return inventory.TryAddItem(inventoryname, item.Name);
		else
		{
			var dropItem = inventory.GetItem(inventoryname, 0);
			DropItem(dropItem);
			inventory.RemoveItemPos(inventoryname, 0, 1);
			return inventory.TryAddItem(inventoryname, item.Name);
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
