using UnityEngine;

public class ShowKeyImage : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private ItemPickup item;
	private Camera camera;

    void Awake()
    {
		item = GetComponent<ItemPickup>();
        item.OnChangeIsPicked += SetEnableCanvas;
		camera = Camera.main;

		if (canvas)
			canvas.worldCamera = camera;
	}

	private void SetEnableCanvas()
	{
		if (canvas != null)
			canvas.enabled = item.IsPicked ? false : true;
	}

	private void LateUpdate()
	{
		RotateHPCanvas();
	}

	private void RotateHPCanvas()
	{
		if (canvas && canvas.worldCamera != null)
		{
			Quaternion vec = camera.transform.rotation;
			canvas.transform.rotation = vec;
		}
	}
}
