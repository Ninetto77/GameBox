using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static CraftItem;

public class TypeCtaftItemButton : MonoBehaviour
{
	public CraftType craftType;
	[Inject] CraftManager manager;
	[Inject] DiContainer container;

	void Start()
    {
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(onClick);
    }

	private void onClick()
	{
		for (int i = 0; i < manager.craftItemsPanel.childCount; i++)
		{
			Destroy(manager.craftItemsPanel.GetChild(i).gameObject);
		}
		foreach (CraftItem item in manager.allCrafts)
		{
			if (item.craftType == craftType)
			{
				GameObject craftItemButton = container.InstantiatePrefab(manager.craftItemButtonPrefab, manager.craftItemsPanel);
				craftItemButton.transform.GetChild(0).GetComponent<Text>().text = item.finalCraft.Name;

				craftItemButton.GetComponent<FillCraftItemDetails>().currentCraftItem = item;
			}
		}
	}
}
