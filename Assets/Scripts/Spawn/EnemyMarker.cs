using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
   public EnemyType EnemyType;
	private void OnDrawGizmos()
	{
		switch (EnemyType)
		{
			case EnemyType.simpolZombi:
				Gizmos.color = Color.yellow;
				break;
			case EnemyType.hardZombi:
				Gizmos.color = Color.red;
				break;
			default:
				break;
		}
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.color = Color.white;
	}
}
