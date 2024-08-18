using UnityEngine;
using UnityEngine.UI;
using Zenject;



public class ItemPickup : Interactable
{
    public ItemInfo item;
    public bool isPicked = false;
	[Inject] private Inventory inventory;

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
        if (!isPicked)
        {
            Debug.Log("Pick Item " + item.name);

			isPicked = inventory.TryAddItem(item);

            if (isPicked)
                Destroy(this.gameObject);

        }
    }

	/// <summary>
	/// Remove from inventory
	/// </summary>
	public void RemoveItem()
    {
        inventory.RemoveItem(item);
        isPicked = false;
    }
}
