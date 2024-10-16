using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	[SerializeField] private float timeToDestroy;

	private void Awake()
	{
		Destroy(this.gameObject, timeToDestroy);
	}
}
