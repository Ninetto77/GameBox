using Enemy.States;

namespace ObjectPoolZenject
{
	// Абстрактный базовый класс для пулов врагов
	public abstract class EnemyPool : MyMemoryPool<MyPoolObject>
	{
		protected override void OnCreateItem(MyPoolObject enemy)
		{
			enemy.OnCreate();
		}
		protected override void OnBeforeSpawned(MyPoolObject enemy)
		{
			enemy.OnSpawned();
		}

		protected override void OnAfterDespawned(MyPoolObject enemy)
		{
			enemy.OnDespawned();
		}

	}
}