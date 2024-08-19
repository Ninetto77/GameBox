using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FillCraftItemDetails : MonoBehaviour
{
	public CraftItem currentCraftItem { get; set; }
	[Inject] private CraftManager craftManager;


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
		var resourcePanelTransform = craftManager.craftItemResourcePanel.transform;

		for (int i = 0; i < resourcePanelTransform.childCount; i++)
		{
			Destroy(resourcePanelTransform.GetChild(i).gameObject);
		}


		var item = currentCraftItem.finalCraft;

		craftManager.FillCraftItemDetails(
			item.Name,
			item.Description,
			item.Icon,
			currentCraftItem.craftTime,
			currentCraftItem.craftAmount);


		//bool canCraft = true;
		for (int i = 0; i < currentCraftItem.craftResources.Count; i++)
		{
			GameObject craftResourceGO = Instantiate(craftManager.craftItemResourceButtonPrefab, resourcePanelTransform);


			CraftResourceDetails crd = craftResourceGO.GetComponent<CraftResourceDetails>();
			if (crd != null)
			{
				crd.FillResourceDetails(
					currentCraftItem.craftResources[i].craftObjectAmount.ToString(),
					currentCraftItem.craftResources[i].craftObject.Name,
					currentCraftItem.craftResources[i].craftObjectAmount.ToString(),
					"0"
					);
			}
			else
				Debug.LogAssertion($"Нет инфы по поводу ресурсов {currentCraftItem.craftResources[i].craftObject.Name}");
		}
	}
}