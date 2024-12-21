using UnityEngine;

public class DeleteTriggerCollider : MonoBehaviour
{
    private bool toolIsPicked;
	private ItemPickup item;
	private BoxCollider colliterWithTrigger;

	void Start()
	{
		item = GetComponent<ItemPickup>();
		colliterWithTrigger = GetComponent<BoxCollider>();

		ChangeIsPicked();
		item.OnChangeIsPicked += ChangeIsPicked;
	}

	private void ChangeIsPicked()
	{
		if (colliterWithTrigger != null && item != null)
			colliterWithTrigger.enabled = item.IsPicked ? false : true;
	}
}
