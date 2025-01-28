using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
   public EnemyType type;
	private void OnDrawGizmos()
	{
		switch (type)
		{
			case EnemyType.simpolZombi:
				Gizmos.color = Color.yellow;
				break;
			case EnemyType.hardZombi:
				Gizmos.color = Color.red;
				break;
			case EnemyType.witch:
				Gizmos.color = Color.black;
				break;
			case EnemyType.spider:
				Gizmos.color = Color.blue;
				break;
			case EnemyType.skeleton:
				Gizmos.color = Color.magenta;
				break;
			//case EnemyType.mainWitch:
			//	Gizmos.color = Color.green;
			//	break;
			default:
				break;
		}
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.color = Color.white;
	}
}
