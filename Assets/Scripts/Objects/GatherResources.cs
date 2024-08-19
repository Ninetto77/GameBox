using System;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemPickup))]
public class GatherResources : MonoBehaviour
{
	public float DistanceToInteract = 1.5f;
	private FXType fxPrefab;

    private LayerMask layerMask;
	private ItemInfo itemPickup;
	private bool insrumentIsPicked;
	private Tool insrument;
    private Camera mainCamera;
	[Inject] private Inventory inventory;
    private OutlineObjects outline;
	FXProvider fXProvider;

    private void Start()
    {
       // inventory = Inventory.instance;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

		var temp = gameObject.GetComponent<ItemPickup>();
		itemPickup = temp.item;
		insrumentIsPicked = temp.isPicked;

		fXProvider = new FXProvider();
		InitInstrument();

	}

	private void InitInstrument()
	{
		insrument =(Tool)itemPickup;
		layerMask = insrument.LayerMaskToHit;
		fxPrefab  = insrument.FXType;
	}

	public void TryGatherResource()
	{
		if (!insrumentIsPicked) return;

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
			inventory.TryAddItem(resource.PartOfResource);

			GetFX(hit);
			//if (FXPrefab!=null) 
			//	Instantiate(FXPrefab, transform.position, Quaternion.Euler(hit.normal));

			resource.TakeDamage(insrument.Power);
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
}
