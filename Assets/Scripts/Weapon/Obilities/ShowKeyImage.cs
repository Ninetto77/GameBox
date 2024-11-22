using Cache;
using UnityEngine;

public class ShowKeyImage : MonoCache
{
    [SerializeField] private Canvas canvas;

    private ItemPickup item;
	private Camera camera;
	private bool canTakeWeapon;
	private OutlineObjects lastOutline;


	void Awake()
    {
		item = GetComponent<ItemPickup>();
        item.OnChangeIsPicked += SetEnableCanvas;
		camera = Camera.main;
		lastOutline = GetComponent<OutlineObjects>();
		canTakeWeapon = false;

		if (canvas)
		{
			canvas.worldCamera = camera;
			canvas.enabled = false;
		}
	}

	protected override void OnTick()
	{
		if (canTakeWeapon)
		{
			GetWeapon();
		}
	}

	private void LateUpdate()
	{
		RotateHPCanvas();
	}

	private void SetEnableCanvas()
	{
		if (canvas != null)
			canvas.enabled = item.IsPicked ? false : true;
	}
	private void RotateHPCanvas()
	{
		if (canvas && canvas.worldCamera != null)
		{
			Quaternion vec = camera.transform.rotation;
			canvas.transform.rotation = vec;
		}
	}
	private void GetWeapon()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (item != null)
				item.Interact();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (item.IsPicked == true)
			return;

		if (!other.gameObject.CompareTag("Player"))
			return;

		if (canvas != null)
			canvas.enabled = true;
		canTakeWeapon = true;
		item.outline.enabled = true;

	}

	private void OnTriggerExit(Collider other)
	{
		if (item.IsPicked == true)
			return;

		if (!other.gameObject.CompareTag("Player"))
			return;

		if (canvas != null)
			canvas.enabled = false;
		canTakeWeapon = false;
		item.outline.enabled = false;

	}
}
