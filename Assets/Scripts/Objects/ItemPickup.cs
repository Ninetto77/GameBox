
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
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

            isPicked = Inventory.instance.TryAddItem(item);

            if (isPicked)
                Destroy(this.gameObject);

        }

    }

    /// <summary>
    /// Remove from inventory
    /// </summary>
    public void RemoveItem()
    {
        Inventory.instance.RemoveItem(item);
        isPicked = false;
    }
}
