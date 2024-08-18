using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static CraftItem;

public class TypeCtaftItemButton : MonoBehaviour
{
	public CraftType craftType;
	[Inject] CraftManager manager;

	void Start()
    {
        Button btn = GetComponent<Button>();
		//btn.onClick += onClick();
    }

	private void onClick()
	{
		for (int i = 0; i < manager.craftItemsPanel.childCount; i++)
		{
			Destroy(manager.craftItemsPanel.GetChild(i).gameObject);
		}
		foreach (CraftItem cso in manager.allCrafts)
		{
			if (cso.craftType == craftType)
			{
				GameObject craftItemButton = Instantiate(manager.craftItemButtonPrefab, manager.craftItemsPanel);
				//craftItemButton.GetComponent<Image>().sprite = cso.finalCraft.icon;
				craftItemButton.GetComponent<FillCraftItemDetails>().currentCraftItem = cso;
			}
		}
	}
}
