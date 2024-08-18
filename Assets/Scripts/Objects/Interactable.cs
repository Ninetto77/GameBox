using UnityEngine;

[RequireComponent(typeof(OutlineObjects))]
public class Interactable : MonoBehaviour
{
	[HideInInspector]
	public OutlineObjects outline;

	private void Start()
	{
		outline = GetComponent<OutlineObjects>();

		if (outline != null)
			outline.enabled = false;
	}
	public virtual void Interact()
    {
        Debug.Log("Interact ");
    }


}
