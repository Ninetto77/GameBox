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
		//private static Random rand;

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
					_container.InstantiatePrefab(simpolZombi, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), null);
					break;
				case EnemyType.hardZombi:
					_container.InstantiatePrefab(hardZombi, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), null);
					break;
				case EnemyType.skeleton:
					_container.InstantiatePrefab(skeletonObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), null);
					break;
				case EnemyType.witch:
					_container.InstantiatePrefab(witchObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), null);
					break;
				case EnemyType.spider:
					_container.InstantiatePrefab(spiderObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), null);
					break;
				default:
					break;
			}
		}
		private void RandomRotate()
		{
			Quaternion rot = Quaternion.EulerAngles(0, Random.RandomRange(0, 179), 0);
		}
	}
}