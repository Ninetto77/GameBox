using UnityEngine;

public class PlayerBrain
{
	public float reachDistance = 1.5f;

	private OutlineObjects lastOutline;
	private Camera mainCamera;

    public PlayerBrain()
    {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		reachDistance = 3f;
	}

	/// <summary>
	/// Метод собирания предметов в инвентарь
	/// </summary>
	public void GatherResource()
	{
		Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
		RaycastHit hit;

		if (lastOutline != null)
			lastOutline.enabled = false;

		if (Physics.Raycast(ray, out hit, reachDistance))
		{
			ItemPickup item = hit.collider.gameObject.GetComponent<ItemPickup>();

			if (item != null)
			{
				lastOutline = item.outline;
				item.outline.enabled = true;
				if (Input.GetMouseButtonDown(0))
				{
					item.Interact();
				}
			}
			else if (lastOutline != null)
			{
				lastOutline.enabled = false;
				lastOutline = null;
			}
		}
	}
}
