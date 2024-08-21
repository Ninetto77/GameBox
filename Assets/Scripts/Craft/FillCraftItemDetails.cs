using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FillCraftItemDetails : MonoBehaviour
{
	public CraftItem currentCraftItem { get; set; }
	[Inject] private CraftManager craftManager;
	[Inject] private Inventory inventory;
	private bool canCraft = true;


	//public GameObject craftResourcePrefab;
	//public string craftInfoPanelName;
	//private GameObject craftInfoPanelGO;
	//public CraftQueueManager craftQueueManager;

	private void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(FillItemDetails);
	}

	public void FillItemDetails()
	{
		var item = currentCraftItem.finalCraft;

		craftManager.FillCraftItemDetails(
			item.Name,
			item.Description,
			item.Icon,
			currentCraftItem.craftTime,
			currentCraftItem.craftAmount);

		FillCraftResources();

		craftManager.craftButton.interactable = canCraft;
	}

	/// <summary>
	/// Заполнить ресурсы крафта
	/// </summary>
	/// <param name="resourcePanelTransform"></param>
	private void FillCraftResources()
	{
		var resourcePanelTransform = craftManager.craftItemResourcePanel.transform;

		///Очистка панели ресурсов крафта
		for (int i = 0; i < resourcePanelTransform.childCount; i++)
		{
			Destroy(resourcePanelTransform.GetChild(i).gameObject);
		}

		//перебор ресурсов
		for (int i = 0; i < currentCraftItem.craftResources.Count; i++)
		{
			GameObject craftResourceGO = Instantiate(craftManager.craftItemResourceButtonPrefab, resourcePanelTransform);

			//заполнение информации о ресурсах
			CraftResourceDetails crd = craftResourceGO.GetComponent<CraftResourceDetails>();
			if (crd != null)
			{
				var amount = currentCraftItem.craftResources[i].craftObjectAmount;
				var craftItemName = currentCraftItem.craftResources[i].craftObject.name;
				var totalAmount = currentCraftItem.craftResources[i].craftObjectAmount;
				var have = inventory.GetCountOfItem(craftItemName);

				crd.FillResourceDetails(
					amount.ToString(),
					craftItemName,
					totalAmount.ToString(),
					have.ToString()
					);

				if (have < totalAmount)
				{
					canCraft = false;
				}
			}
			else
				Debug.LogAssertion($"Нет инфы по поводу ресурсов {currentCraftItem.craftResources[i].craftObject.Name}");
		}
	}
}