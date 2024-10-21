using UnityEngine;

public class ShowKeyImage : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private ItemPickup item;

    void Awake()
    {
		item = GetComponent<ItemPickup>();
        item.OnChangeIsPicked += SetEnableCanvas;
	}

	private void SetEnableCanvas()
	{
		if (canvas != null)
			canvas.enabled = item.IsPicked ? false : true;
	}

}
