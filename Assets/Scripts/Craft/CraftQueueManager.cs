using UnityEngine;
using TMPro;
using Zenject;

public class CraftQueueManager : MonoBehaviour
{
	[Header("Детали очереди")]
	public GameObject craftQueuePrefab;
	public TMP_Text craftAmountText;

	[HideInInspector]
	public CraftItem currentCraftItem {  get; set; }
	//public Button addButton;
	//public Button removeButton;

	[Inject] private Inventory inventory;
	[Inject] private CraftManager craftManager;


	private int craftTime;

	public void RemoveButtonFunction()
	{
		if (int.Parse(craftAmountText.text) <= 1)
			return;
		int newAmount = int.Parse(craftAmountText.text) - 1;
		craftAmountText.text = newAmount.ToString();
	}
	public void AddButtonFunction()
	{
		if (int.Parse(craftAmountText.text) >= 6)
			return;
		int newAmount = int.Parse(craftAmountText.text) + 1;
		craftAmountText.text = newAmount.ToString();
	}

	public void AddToCraftQueue()
	{
		foreach (CraftResource craftResource in currentCraftItem.craftResources)
		{

			//int amountToRemove = craftResource.craftObjectAmount * int.Parse(craftAmountInputField.text);
			//foreach (InventorySlot slot in inventory.slots)
			//{
			//	if (amountToRemove <= 0)
			//		continue;
			//	if (slot.item == craftResource.craftObject)
			//	{
			//		if (amountToRemove > slot.amount)
			//		{
			//			amountToRemove -= slot.amount;
			//			slot.GetComponentInChildren<DragAndDropItem>().NullifySlotData();
			//		}
			//		else
			//		{
			//			slot.amount -= amountToRemove;
			//			amountToRemove = 0;
			//			if (slot.amount <= 0)
			//			{
			//				slot.GetComponentInChildren<DragAndDropItem>().NullifySlotData();
			//			}
			//			else
			//			{
			//				slot.itemAmountText.text = slot.amount.ToString();
			//			}
			//		}
			//	}
			//}
		}

		//for (int i = 0; i < transform.childCount; i++)
		//{
		//	if (transform.GetChild(i).GetComponent<CraftQueueItemDetails>().currentCraftItem == currentCraftItem)
		//	{
		//		transform.GetChild(i).GetComponent<CraftQueueItemDetails>().craftAmount += int.Parse(craftAmountText.text);
		//		transform.GetChild(i).GetComponent<CraftQueueItemDetails>().amountText.text = "X" + transform.GetChild(i).GetComponent<CraftQueueItemDetails>().craftAmount.ToString();
		//		craftManager.currentCraftItem.FillItemDetails();
		//		return;
		//	}
		//}

		GameObject craftQueueInstance = Instantiate(craftQueuePrefab, transform);
		
		///отображение деталей предмета в очереди в UI
		CraftQueueItemDetails craftQueueItemDetails = craftQueueInstance.GetComponent<CraftQueueItemDetails>();

		if (craftQueueItemDetails == null)
		{
			Debug.Log("Нет деталей очереди CraftQueueItemDetails");
			return;
		}

		GetCraftTime(out int minutes, out int seconds);
		craftQueueItemDetails.FillGUADetails(
											craftAmountText.text,
											minutes, seconds,
											currentCraftItem.finalCraft.Icon);

		craftQueueItemDetails.FillCraftQueueDetails(
									craftAmountText.text,
									craftTime,
									currentCraftItem
									);

		craftManager.currentCraftItem.FillItemDetails();
	}

	private void GetCraftTime(out int minutes, out int seconds)
	{
		craftTime = currentCraftItem.craftTime;
		minutes = Mathf.FloorToInt(craftTime / 60);
		seconds = craftTime - minutes * 60;
	}
}	