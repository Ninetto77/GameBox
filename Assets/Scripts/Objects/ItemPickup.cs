using InventorySystem;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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

			if (!inventory.InventoryFull(inventoryname, item.Name))
                isPicked = inventory.TryAddItem(inventoryname, item.Name);
			else
            {
				Instantiate(inventory.GetItem(inventoryname, 0).GetRelatedGameObject(), new Vector3(40, 2,50), Quaternion.identity);
		        inventory.RemoveItemPos(inventoryname, 0, 1);
				isPicked = inventory.TryAddItem(inventoryname, item.Name);
			}

			Debug.Log($"In {inventoryname} is 0 " + inventory.GetItem(inventoryname, 0));


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
