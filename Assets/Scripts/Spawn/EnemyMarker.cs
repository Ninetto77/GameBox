using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
   public EnemyType EnemyType;
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.color = Color.white;
	}
}
