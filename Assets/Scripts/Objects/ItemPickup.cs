using System;
using UnityEngine;
using Zenject;

public class ItemPickup : Interactable
{
    public ItemInfo item;
    public Action OnChangeIsPicked;

	private bool isPicked;
    public bool IsPicked { get { return isPicked; } set { isPicked = value; OnChangeIsPicked?.Invoke(); } }

	[Inject] private ItemsController itemsController;

	public override void Interact()
    {
        base.Interact();
        PickItem();
    }   

    /// <summary>
    /// add to inventory
    /// </summary>
    public void PickItem()
    {
        if (!IsPicked)
		{
			Debug.Log("Pick Item " + item.name);

			IsPicked = itemsController.TryAddToInventory(item);

			if (IsPicked)
				Destroy(this.gameObject);

		}
	}
}
