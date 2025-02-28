using Cache;
using InventorySystem;
using Items;
using Points;
using UnityEngine;
using Zenject;

public class ItemsController : MonoCache
{
	[Inject] private EquipmentManager equipmentManager;

	private ItemInfo item;
	private InventoryController inventory;
	private readonly string ActiveSlotName = GlobalStringsVars.ACTIVESLOT_NAME;
	
	[Inject] private CartridgeShop shop;
	[Inject] private BulletUI bulletUI;

	private void Start()
	{
		inventory = InventoryController.instance;
	}

	protected override void OnTick()
	{
		string inventoryname = "";

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			inventoryname = InventoryType.GetInventoryName(ItemType.melle);
			SetActiveSlot(inventoryname);
			CheckHandForChangeBulletUI(TypeOfCartridge.none, true);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			inventoryname = InventoryType.GetInventoryName(ItemType.lightWeapon);
			SetActiveSlot(inventoryname);
			CheckHandForChangeBulletUI(TypeOfCartridge.light, true);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			inventoryname = InventoryType.GetInventoryName(ItemType.heavyWeapon);
			SetActiveSlot(inventoryname);
			CheckHandForChangeBulletUI(TypeOfCartridge.heavy, true);
		}
	}

	/// <summary>
	/// Обновить количество пуль
	/// </summary>
	/// <param name="typeOfWeapon"></param>
	private void UpdateBulletUI(ItemType typeOfWeapon, bool update)
	{
		if (update)
		{
			switch (typeOfWeapon)
			{
				case ItemType.melle:
					bulletUI.OnChangeBullets?.Invoke(-1, -1);
					break;
				case ItemType.lightWeapon:
					bulletUI.OnChangeBullets?.Invoke(0, shop.LightCartridgeCount);
					break;
				case ItemType.heavyWeapon:
					bulletUI.OnChangeBullets?.Invoke(0, shop.HeavyCartridgeCount);
					break;
				case ItemType.scientificWeapon:
					break;
				default:
					break;
			}
		}
		else
		{
			switch (typeOfWeapon)
			{
				case ItemType.melle:
					bulletUI.OnChangeBullets?.Invoke(-1, -1);
					break;
				case ItemType.lightWeapon:
					bulletUI.OnChangeCommonBullets?.Invoke(shop.LightCartridgeCount);
					break;
				case ItemType.heavyWeapon:
					bulletUI.OnChangeCommonBullets?.Invoke(shop.HeavyCartridgeCount);
					break;
				case ItemType.scientificWeapon:
					break;
				default:
					break;
			}
		}	
	}

	/// <summary>
	/// Установить активный слот
	/// </summary>
	/// <param name="inventoryname"></param>
	private void SetActiveSlot(string inventoryname)
	{
		var itemInInventory = inventory.GetInventory(inventoryname).InventoryGetItem(0).GetItemType();
		
		if (CheckIsDifferentItems(itemInInventory) == false)
			return;

		equipmentManager.UnequipHand();
		CleanActiveSlot();

		if (itemInInventory != null)
		{
			inventory.AddItemPos(ActiveSlotName, itemInInventory, 0);
		}

	}

	/// <summary>
	/// Проверяет, не в руках уже данное оружие
	/// </summary>
	/// <param name="itemInInventory"></param>
	/// <returns></returns>
	private bool CheckIsDifferentItems(string itemInInventory)
	{
		string curWeaponInActiveSlot = inventory.GetInventory(ActiveSlotName).InventoryGetItem(0).GetItemType();

		if (curWeaponInActiveSlot != null)
		{
			ItemType curTypeWeaponInActiveSlot = inventory.GetInventory(ActiveSlotName).InventoryGetItem(0).GetItemInfo().ItemType;
			curWeaponInActiveSlot = InventoryType.GetInventoryName(curTypeWeaponInActiveSlot);

			if (curWeaponInActiveSlot == itemInInventory)
				return false;
		}
		return true;
	}

	/// <summary>
	/// Очистить активный слот
	/// </summary>
	private void CleanActiveSlot()
	{
		//Debug.Log("clean active slot in CleanActiveSlot() ");
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
	/// Удалось ли добавить предмет в инвентарь
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool TryAddToInventory(ItemInfo item)
	{
		var type = item.ItemType;
		string inventoryname = InventoryType.GetInventoryName(type);

		bool isPicked = TryAdd(inventoryname, item);

		if (CheckIsDifferentItems(inventoryname) == false)
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

			if (CheckIsDifferentItems(inventoryname) == false)
			{
				//Debug.Log("clean in TryAdd");
				CleanActiveSlot();
			}
			//else
			//	Debug.Log("no clean Try Add");

			DropItem(inventoryname, dropItem);
			inventory.RemoveItemPos(inventoryname, 0, 1);

			return inventory.TryAddItem(inventoryname, item.Name);
		}
	}

	/// <summary>
	/// Бросить в мир объект
	/// </summary>
	/// <param name="pos"></param>
	/// <param name="dropItem"></param>
	public void DropItem(string inventoryname, InventoryItem dropItem)
	{
		if (dropItem == null) return;

		CheckIsMelleWeapon(dropItem);

		for (int i = 0; i < dropItem.GetAmount(); i++)
		{
			//Debug.Log($"Хочу сбросить = {dropItem.GetItemInfo().Name}");

			var objInHand = equipmentManager.GetPlayerHand();
			if (objInHand == null) //если рука пустая
			{
				//Debug.Log($"Рука пустая. Беру в руки = {dropItem.GetItemInfo().Name}");
				objInHand = EquipHand(dropItem);
				//Debug.Log($"Взяла в руки {objInHand.name}");

			}
			//else
			//	Debug.Log($"Рука не пустая и занята {objInHand.name}");


			//	Debug.Log($"Категория руки равна {objInHand.name}");
			//Debug.Log($"Категория ненужного оружия {dropItem.GetItemInfo().ItemType.ToString()}");

			GameObject oldObj = null;
			string inventoryHandName = InventoryType.GetInventoryName(objInHand.GetComponent<ItemPickup>().item.ItemType);
			if (inventoryHandName != inventoryname)
			{
				//Debug.Log($"Категория не равны");
				//Debug.Log($"Меняю руку");

				oldObj = equipmentManager.GetPlayerHand();

				objInHand = EquipHand(dropItem);
				//	Debug.Log($"Взяла в руки {objInHand.name}");

				SetSettingsToDrop(objInHand, false);

				//				Debug.Log($"Сбросила руку");
				equipmentManager.DropHand();
				EquipHand(oldObj.GetComponent<ItemPickup>().item);
				//Debug.Log($"Взяла в руку старое оружие " + oldObj.name);

			}
			else
			{
				//Debug.Log($"Категории равны");
				SetSettingsToDrop(objInHand, true);

				//Debug.Log($"Сбросила руку");
				equipmentManager.DropHand();
			}

			//SetSettingsToDrop(objInHand);

			//Debug.Log($"Сбросила руку");
			//equipmentManager.DropHand();

		}
	}

	private void CheckIsMelleWeapon(InventoryItem dropItem)
	{
		if (dropItem.GetItemInfo().ItemType == ItemType.melle)
			UpdateBulletUI(ItemType.melle, false);
	}

	private GameObject EquipHand(InventoryItem item)
	{
		equipmentManager.EquipHand(item.GetItemInfo() as Equipable);
		GameObject objInHand = equipmentManager.GetPlayerHand();
		return objInHand;
	}

	private GameObject EquipHand(ItemInfo info)
	{
		equipmentManager.EquipHand(info as Equipable);
		GameObject objInHand = equipmentManager.GetPlayerHand();
		return objInHand;
	}

	/// <summary>
	/// Убрать с руки оружие, сделать его физическим
	/// </summary>
	/// <param name="objInHand"></param>
	/// <returns></returns>
	private void SetSettingsToDrop(GameObject objInHand, bool canUpdateBulletUI)
	{
		ItemPickup dropedItem = objInHand.gameObject.GetComponent<ItemPickup>();

		if (dropedItem != null)
		{
			dropedItem.IsPicked = false;
			dropedItem.GetComponent<Rigidbody>().isKinematic = false;
		}
			
		else
			Debug.Log("dropedItem is null");

		if (canUpdateBulletUI)
			UpdateBulletUI(dropedItem.item.ItemType, true);
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

	/// <summary>
	/// Обновить UI пуль при подборе пуль, если это оружие в руке
	/// </summary>
	/// <param name="type"></param>
	public void CheckHandForChangeBulletUI(TypeOfCartridge type, bool update)
	{
		string obj = inventory.GetInventory(ActiveSlotName).InventoryGetItem(0).GetItemType();
		if (obj != null)
		{
			ItemType handType = inventory.GetItem(ActiveSlotName, 0).GetItemInfo().ItemType;
			switch (type)
			{
				case TypeOfCartridge.light:
					if (handType == ItemType.lightWeapon)
						UpdateBulletUI(handType, update);
					break;
				case TypeOfCartridge.heavy:
					if (handType == ItemType.heavyWeapon)
						UpdateBulletUI(handType, update);
					break;
				case TypeOfCartridge.oil:
					break;
				case TypeOfCartridge.none:
					if (handType == ItemType.melle)
						UpdateBulletUI(handType, update);
					break;
				default:
					break;
			}
		}
		else
			bulletUI.OnChangeBullets?.Invoke(-1, -1);
	}
}
