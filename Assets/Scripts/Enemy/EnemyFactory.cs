using UnityEngine;
using Zenject;

namespace Enemy
{
	public class EnemyFactory : IEnemyFactory
	{
		private const string enemyEasy = "EnemyEasy";
		private const string enemyHard = "EnemyHard";
		private Object simpolZombi;
		private Object hardZombi;
		
		private DiContainer _container;

		public EnemyFactory(DiContainer container)
        {
            _container = container;
        }
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