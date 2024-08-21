using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CraftManager : MonoBehaviour
{
	public bool isOpened;

	[Header("Craft Panels")]
	public Transform craftItemsPanel;
	public Transform craftItemResourcePanel;

	[Header("Craft Buttons Prefab")]
	public GameObject craftItemButtonPrefab;
	public GameObject craftItemResourceButtonPrefab;

	[Header("Craft Buttons")]
	public Button craftButton;

	//public GameObject UIBG;
	//public GameObject crosshair;
	//public Button craftBtn;
	public FillCraftItemDetails currentCraftItem;

	public KeyCode openCloseCraftButton;

	public List<CraftItem> allCrafts;

	[Header("Craft Item Details")]
	public TMP_Text craftItemName;
	public TMP_Text craftItemDescription;
	public Image craftItemImage;
	public TMP_Text craftItemDuration;
	public TMP_Text craftItemAmount;
	
	
	[Inject] private StateMashine stateMashine;
	
	void Start()
	{
		//GameObject craftItemButton = Instantiate(craftItemButtonPrefab, craftItemsPanel);
		//craftItemButton.GetComponent<Image>().sprite = allCrafts[0].finalCraft.Icon;
		//craftItemButton.GetComponent<FillCraftItemDetails>().currentCraftItem = allCrafts[0];
		//craftItemButton.GetComponent<FillCraftItemDetails>().FillItemDetails();
		//Destroy(craftItemButton);

		//craftingPanel.gameObject.SetActive(false);
	}
	/// <summary>
	/// Заполнение детали предмета
	/// </summary>
	/// <param name="name">Имя</param>
	/// <param name="description">Описание</param>
	/// <param name="icon">иконка</param>
	/// <param name="time">время создания</param>
	/// <param name="amount">количество</param>
	public void FillCraftItemDetails(string name, string description, Sprite icon, int time, int amount)
	{
		craftItemName.text = name;
		craftItemDescription.text = description;
		craftItemImage.sprite = icon;
		craftItemDuration.text = time.ToString();
		craftItemAmount.text = amount.ToString();
	}

	void Update()
	{
		if (Input.GetKeyDown(openCloseCraftButton))
		{
			isOpened = !isOpened;
			stateMashine.ChangeStates(StateMashine.StateType.craft);

			if (isOpened)
			{

				// Прекрепляем курсор к середине экрана
				Cursor.lockState = CursorLockMode.None;
				// и делаем его видимым
				Cursor.visible = true;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}



	public void LoadCraftItems(string craftType)
	{
		for (int i = 0; i < craftItemsPanel.childCount; i++)
		{
			Destroy(craftItemsPanel.GetChild(i).gameObject);
		}
		foreach (CraftItem cso in allCrafts)
		{
			if (cso.craftType.ToString().ToLower() == craftType.ToLower())
			{
				GameObject craftItemButton = Instantiate(craftItemButtonPrefab, craftItemsPanel);
				craftItemButton.GetComponent<FillCraftItemDetails>().currentCraftItem = cso;
			}
		}
	}
}