using UnityEngine;

namespace Enemy
{
	public interface IEnemyFactory
	{
		void Load();

		void Create(EnemyType enemyType, Vector3 at);
	}
}