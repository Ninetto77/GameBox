using InventorySystem;
using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemInfo item;
    public bool isPicked = false;

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

            var type = item.ItemType;

            string inventoryname = InventoryType.GetInventoryName(type);

            isPicked = InventoryController.instance.TryAddItem(inventoryname, item.Name);

            if (isPicked)
                Destroy(this.gameObject);

        }
    }

	///// <summary>
	///// Remove from inventory
	///// </summary>
	//public void RemoveItem()
 //   {
	//	InventoryController.instance.RemoveItem(inventoryname, item.Name, 1);
 //       isPicked = false;
 //   }
}
