using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Zenject;

public class CraftManager : MonoBehaviour
{
	public bool isOpened;

	public Transform craftItemsPanel;
	public GameObject craftItemButtonPrefab;

	public GameObject UIBG;
	public GameObject crosshair;
	public Button craftBtn;
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
	public void FillItemDetailsHelper()
	{
		currentCraftItem.FillItemDetails();
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(openCloseCraftButton))
		{
			isOpened = !isOpened;
			stateMashine.ChangeStates(StateMashine.StateType.craft);

			if (isOpened)
			{

				// ����������� ������ � �������� ������
				Cursor.lockState = CursorLockMode.None;
				// � ������ ��� �������
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
				//craftItemButton.GetComponent<Image>().sprite = cso.finalCraft.icon;
				craftItemButton.GetComponent<FillCraftItemDetails>().currentCraftItem = cso;
			}
		}
	}
}