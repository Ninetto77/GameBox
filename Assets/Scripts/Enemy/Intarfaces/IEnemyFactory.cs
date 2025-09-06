using UnityEngine;

namespace Enemy
{
	public interface IEnemyFactory
	{
		void Load();

		GameObject Create(EnemyType enemyType, Vector3 at, Transform parent = null);
	}
}