using UnityEngine;
using Zenject;

namespace Enemy
{
	public class EnemyFactory : MonoBehaviour, IEnemyFactory
	{
		private const string enemyEasy = "SimpolZombi";
		private const string enemyHard = "HardZombi";
		private const string spider = "Spider";
		private const string skeleton = "Skeleton";
		private const string witch = "Witch";

		private Object simpolZombi;
		private Object hardZombi;
		private Object skeletonObject;
		private Object witchObject;
		private Object spiderObject;
		
		[Inject] private DiContainer _container;

		public void Load()
		{
			simpolZombi = Resources.Load(enemyEasy);
			hardZombi = Resources.Load(enemyHard);
			skeletonObject = Resources.Load(skeleton);
			witchObject = Resources.Load(witch);
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
				case EnemyType.skeleton:
					_container.InstantiatePrefab(skeletonObject, at, Quaternion.identity, null);
					break;
				case EnemyType.witch:
					_container.InstantiatePrefab(witchObject, at, Quaternion.identity, null);
					break;
				case EnemyType.spider:
					_container.InstantiatePrefab(spiderObject, at, Quaternion.identity, null);
					break;
				default:
					break;
			}
		}
	}

}