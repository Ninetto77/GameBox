using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using InventorySystem;
using Zenject.ReflectionBaking.Mono.Cecil;

public class CraftQueueItemDetails : MonoBehaviour
{
	[Header("Детали предмета в очереди")]
	public TMP_Text amountText;
	public TMP_Text timeText;
	public Image itemImage;

	private CraftItem currentCraftItem;
	private int craftTime;
	private int craftAmount;
	private int initialCraftTime;
	
	[Inject] private CraftManager craftManager;
	//[Inject] private Inventory inventory;
	private readonly string inventoryname = GlobalStringsVars.INVENTORY_NAME;
	private InventoryController inventory;

	private void Start()
	{
		initialCraftTime = craftTime;
		craftTime++;
		if (transform.parent.childCount <= 1)
		{
			InvokeRepeating("UpdateTime", 0f, 1f);
		}
		else
		{
			UpdateTime();
		}
		inventory = InventoryController.instance;
	}

	public void FillCraftQueueDetails(string amount, int time, CraftItem item)
	{
		craftAmount = int.Parse(amount);

		craftTime = time;
		currentCraftItem = item;
	}

	public void FillGUADetails(string amount, int minutes, int seconds, Sprite image)
	{
		amountText.text = amount;
		timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		itemImage.sprite = image;
	}

	public void StartInvoke()
	{
		InvokeRepeating("UpdateTime", 0f, 1f);
	}

	/// <summary>
	/// Когда время закончилось, добавить предмет в инвентарь
	/// </summary>
	public void RemoveFromQueue()
	{
		foreach (CraftResource resource in currentCraftItem.craftResources)
		{
			//inventory.TryAddItems(resource.craftObject, resource.craftObjectAmount * craftAmount);
			inventory.AddItem(inventoryname, resource.craftObject.Name, resource.craftObjectAmount * craftAmount);
		}
		CancelInvoke();
		craftManager.currentCraftItem.FillItemDetails();
		if (transform.parent.childCount > 1)
			transform.parent.GetChild(1).GetComponent<CraftQueueItemDetails>().StartInvoke();
		Destroy(gameObject);
	}

	/// <summary>
	/// Обновление времени предмета в очереди
	/// </summary>
	void UpdateTime()
	{
		amountText.text = "X" + craftAmount.ToString();
		craftTime--;
		if (craftTime <= 0)
		{
			//inventory.TryAddItems(currentCraftItem.finalCraft, currentCraftItem.craftAmount);
			inventory.AddItem(inventoryname, currentCraftItem.finalCraft.Name, currentCraftItem.craftAmount);

			craftAmount--;
			craftTime = initialCraftTime;
			if (craftAmount <= 0)
			{
				CancelInvoke();
				if (transform.parent.childCount > 1)
					transform.parent.GetChild(1).GetComponent<CraftQueueItemDetails>().StartInvoke();
				Destroy(gameObject);
			}
		}
		else
		{
			int minutes = Mathf.FloorToInt(craftTime / 60);
			int seconds = craftTime - minutes * 60;
			timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}
}