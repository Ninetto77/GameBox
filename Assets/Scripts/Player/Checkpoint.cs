using UnityEngine;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour
{
	static List<Vector3> collectedCheckpoints = new List<Vector3>();

	private void Start()
	{
		if (collectedCheckpoints.Count > 0)
		{
			transform.position = collectedCheckpoints[collectedCheckpoints.Count - 1];
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Checkpoint") && !IsCheckpointCollected(collision.gameObject.transform.position))
		{
			collectedCheckpoints.Add(collision.gameObject.transform.position);
		}


		bool IsCheckpointCollected(Vector3 checkingCheckpointPosition)
		{
			if (collectedCheckpoints.Count > 0)
			{
				foreach (Vector3 collectedCheckpoint in collectedCheckpoints)
				{
					if (checkingCheckpointPosition == collectedCheckpoint)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}