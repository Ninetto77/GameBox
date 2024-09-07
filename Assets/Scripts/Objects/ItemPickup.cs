using InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;



public class ItemPickup : Interactable
{
    public ItemInfo item;
    public bool isPicked = false;
	//[Inject] private Inventory inventory;

	private readonly string inventoryname = GlobalStringsVars.INVENTORY_NAME;

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

			isPicked = InventoryController.instance.TryAddItem(inventoryname, item.Name);

            if (isPicked)
                Destroy(this.gameObject);

        }
    }

	/// <summary>
	/// Remove from inventory
	/// </summary>
	public void RemoveItem()
    {
		InventoryController.instance.RemoveItem(inventoryname, item.Name, 1);
        isPicked = false;
    }
}
