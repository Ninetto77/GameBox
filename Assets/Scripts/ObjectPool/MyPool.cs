using UnityEngine;

namespace ObjectPoolZenject
{
	public static class MyPool
    {
		private static Transform globalParent;

		public static MyPoolObject GetPooledObject(this EnemyPool pool,
											Vector3 at,
											Quaternion rotation,
											Transform parent = null)
		{
			var enemy = pool.Spawn(); // Берем объект из пула Zenject
									  //var zombi = _zombiSimpolPool.Spawn();
			enemy.transform.parent = parent;
			enemy.transform.position = at;
			enemy.transform.rotation = rotation;

			globalParent = parent;
			return enemy;
		}

		public static void ReturnPooledObject(this EnemyPool pool, MyPoolObject enemy)
		{
			pool.Despawn(enemy); // Возвращаем объект в пул Zenject
			enemy.transform.parent = globalParent;
		}
	}
}
