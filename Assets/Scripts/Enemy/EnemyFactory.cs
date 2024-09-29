using UnityEngine;
using Zenject;

namespace Enemy
{
	public class EnemyFactory : MonoBehaviour, IEnemyFactory
	{
		private const string enemyEasy = "SimpolZombi";
		private const string enemyHard = "HardZombi";
		private Object simpolZombi;
		private Object hardZombi;
		
		[Inject] private DiContainer _container;

		public void Load()
		{
			simpolZombi = Resources.Load(enemyEasy);
			hardZombi = Resources.Load(enemyHard);
		}

		public void Create(EnemyType enemyType, Vector3 at)
		{
			switch (enemyType)
			{
				case EnemyType.simpolZombi:
					_container.InstantiatePrefab(simpolZombi, at, Quaternion.identity, null);
					break;
				case EnemyType.hardZombi:
					_container.InstantiatePrefab(hardZombi, at, Quaternion.identity, null);
					break;
				default:
					break;
			}
		}
	}

}