using UnityEngine;
using Zenject;

namespace Enemy
{
	public class EnemyFactory : MonoBehaviour, IEnemyFactory
	{
		private const string enemyEasy = "SimpolZombi";
		private const string enemyHard = "HardZombi";
		private const string spider = "Spider";

		private Object simpolZombi;
		private Object hardZombi;
		private Object spiderObject;
		
		[Inject] private DiContainer _container;

		public void Load()
		{
			simpolZombi = Resources.Load(enemyEasy);
			hardZombi = Resources.Load(enemyHard);
			spiderObject = Resources.Load(spider);
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
				case EnemyType.vampire:
					break;
				case EnemyType.witch:
					break;
				case EnemyType.spider:
					_container.InstantiatePrefab(spiderObject, at, Quaternion.identity, null);
					break;
				case EnemyType.voodoo:
					break;
				default:
					break;
			}
		}
	}

}