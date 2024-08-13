using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
	[HideInInspector]
	public Outline outline;

	private void Start()
	{
		outline = GetComponent<Outline>();

		if (outline != null)
			outline.enabled = false;
	}
	public virtual void Interact()
    {
        Debug.Log("Interact ");
    }


}
