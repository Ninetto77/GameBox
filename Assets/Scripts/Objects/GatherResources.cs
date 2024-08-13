using UnityEngine;

[RequireComponent(typeof(ItemPickup))]
public class GatherResources : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
	public float DistanceToInteract = 1.5f;

	private ItemInfo itemPickup;
	private bool insrumentIsPicked;
	private Insrument insrument;
    private Camera mainCamera;
    private Inventory inventory;
    private Outline outline;

    private void Start()
    {
        inventory = Inventory.instance;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

		var temp = gameObject.GetComponent<ItemPickup>();
		itemPickup = temp.item;
		insrumentIsPicked = temp.isPicked;

		insrument =(Insrument)itemPickup;
    }

    private void Update()
    {
		if (!insrumentIsPicked) return;

		Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
		RaycastHit hit;

        if (Physics.Raycast(ray, out hit, DistanceToInteract, layerMask))
			if (Input.GetMouseButtonDown(0))
				GatherResource(hit);
	}

	/// <summary>
	/// Метод добычи ресурсов с помощью инструментов
	/// </summary>
	public void GatherResource(RaycastHit hit)
	{
		ExtractedResource resource = hit.collider.gameObject.GetComponent<ExtractedResource>();
		if (resource != null)
		{
			inventory.TryAddItem(resource.PartOfResource);
			resource.TakeDamage(insrument.Power);
		}
	}
}
