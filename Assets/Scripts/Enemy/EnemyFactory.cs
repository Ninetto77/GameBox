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
		private const string mainWitch = "MainWitch";

		private Object simpolZombi;
		private Object hardZombi;
		private Object skeletonObject;
		private Object witchObject;
		private Object mainWitchObject;
		private Object spiderObject;

		[Inject] private DiContainer _container;
		//private static Random rand;

		public void Load()
		{
			simpolZombi = Resources.Load(enemyEasy);
			hardZombi = Resources.Load(enemyHard);
			skeletonObject = Resources.Load(skeleton);
			witchObject = Resources.Load(witch);
			mainWitchObject = Resources.Load(mainWitch);
			spiderObject = Resources.Load(spider);
		}

		public void Create(EnemyType enemyType, Vector3 at, Transform parent = null)
		{
			switch (enemyType)
			{
				case EnemyType.simpolZombi:
					_container.InstantiatePrefab(simpolZombi, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.hardZombi:
					_container.InstantiatePrefab(hardZombi, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.skeleton:
					_container.InstantiatePrefab(skeletonObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.witch:
					_container.InstantiatePrefab(witchObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;		
				case EnemyType.mainWitch:
					_container.InstantiatePrefab(mainWitchObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.spider:
					_container.InstantiatePrefab(spiderObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				default:
					break;
			}
		}

	}
}