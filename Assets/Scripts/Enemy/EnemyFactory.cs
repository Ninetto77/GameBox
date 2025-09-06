using ObjectPoolZenject;
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
		private const string manKiller = "ManKiller";
		private const string witch = "Witch";
		private const string mainWitch = "MainWitch";

		private Object simpolZombi;
		private Object hardZombi;
		private Object skeletonObject;
		private Object manKillerObject;
		private Object witchObject;
		private Object mainWitchObject;
		private Object spiderObject;

		[Inject] private DiContainer _container;
		[Inject] private SimpolZombiPool _zombiSimpolPool;
		[Inject] private HardZombiPool _zombiHardPool;
		[Inject] private SpiderPool _spiderPool;
		[Inject] private SkeletonPool _skeletonPool;
		[Inject] private MainWitchPool _mainWitchPool;
		[Inject] private WitchPool _witchPool;
		[Inject] private ManKillerPool _manKillerPool;
		//private static Random rand;

		public void Load()
		{
			simpolZombi = Resources.Load(enemyEasy);
			hardZombi = Resources.Load(enemyHard);
			skeletonObject = Resources.Load(skeleton);
			manKillerObject = Resources.Load(manKiller);
			witchObject = Resources.Load(witch);
			mainWitchObject = Resources.Load(mainWitch);
			spiderObject = Resources.Load(spider);
		}

		public GameObject Create(EnemyType enemyType, Vector3 at, Transform parent = null)
		{
			MyPoolObject enemy = null;
			switch (enemyType)
			{
				case EnemyType.simpolZombi:
					enemy = _zombiSimpolPool.GetPooledObject(at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.hardZombi:
					enemy = _zombiHardPool.GetPooledObject(at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.skeleton:
					enemy = _skeletonPool.GetPooledObject(at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.manKiller:
					enemy = _manKillerPool.GetPooledObject(at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.witch:
					enemy = _witchPool.GetPooledObject(at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;		
				case EnemyType.mainWitch:
					enemy = _mainWitchPool.GetPooledObject(at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				case EnemyType.spider:
					enemy = _spiderPool.GetPooledObject(at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					//_container.InstantiatePrefab(spiderObject, at, Quaternion.Euler(0, Random.Range(0f, 360f), 0), parent);
					break;
				default:
					break;
			}
			return enemy.transform.gameObject;
		}

	}
}