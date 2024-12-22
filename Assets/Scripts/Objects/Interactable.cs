using InventorySystem;
using UnityEngine;

[RequireComponent(typeof(OutlineObjects))]
public class Interactable : MonoBehaviour
{
	[HideInInspector]
	public OutlineObjects outline;
	protected InventoryController inventory;

	private void Start()
	{
		inventory = InventoryController.instance;

		outline = GetComponent<OutlineObjects>();

		if (outline != null)
			outline.enabled = false;
	}
	public virtual void Interact()
    {
      //  Debug.Log("Interact ");
    }


}
