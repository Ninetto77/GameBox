using InventorySystem;
using System;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;

[RequireComponent(typeof(ItemPickup))]
public class GatherResources : MonoBehaviour
{
	public float DistanceToInteract = 1.5f;
	private FXType fxPrefab;

    private LayerMask layerMask;
	private ItemInfo itemPickup;
	private bool toolIsPicked;
	private Tool tool;
    private Camera mainCamera;
    private OutlineObjects outline;
	FXProvider fXProvider;

	private readonly string inventoryname = GlobalStringsVars.INVENTORY_NAME;
	private InventoryController inventory;
	[Inject] EquipmentManager equipmentManager;


	private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

		var temp = gameObject.GetComponent<ItemPickup>();
		itemPickup = temp.item;
		toolIsPicked = temp.isPicked;

		fXProvider = new FXProvider();
		InitInstrument();
		inventory = InventoryController.instance;
	}

	private void InitInstrument()
	{
		tool =(Tool)itemPickup;
		layerMask = tool.LayerMaskToHit;
		fxPrefab  = tool.FXType;
	}

	public void TryGatherResource()
	{
		if (!toolIsPicked) return;

		Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, DistanceToInteract, layerMask))
				GatherResource(hit);
	}

	/// <summary>
	/// Метод добычи ресурсов с помощью инструментов
	/// </summary>
	private void GatherResource(RaycastHit hit)
	{
		ExtractedResource resource = hit.collider.gameObject.GetComponent<ExtractedResource>();
		if (resource != null)
		{
			//inventory.TryAddItem(resource.PartOfResource);
			inventory.AddItem(inventoryname, resource.PartOfResource.Name);

			GetFX(hit);
			//if (FXPrefab!=null) 
			//	Instantiate(FXPrefab, transform.position, Quaternion.Euler(hit.normal));

			resource.TakeDamage(tool.Power);
		}
	}

	private void GetFX(RaycastHit hit)
	{
		if (fxPrefab == FXType.none) return;
		fXProvider.LoadFX(fxPrefab, transform.position, Quaternion.Euler(hit.normal));

		StartCoroutine(UnloadFX());
	}

	private IEnumerator UnloadFX()
	{
		yield return new WaitForSeconds(5f);
		fXProvider.UnloadFX();
	}

	public void EquipmentTool()
	{
		if (tool != null)
		{
			tool.Use(equipmentManager);
		}
	}
}
