using Zenject;
using Enemy;

public class LocationInstaller : MonoInstaller, IInitializable
{
	public EnemyMarker[] enemyMarkers;
	public EnemyFactory enemyFactoryPrefab;

	public void Initialize()
	{
		var enemyFactory = Container.Resolve<IEnemyFactory>();

		enemyFactory.Load();
		foreach (EnemyMarker marker in enemyMarkers)
			enemyFactory.Create(marker.type, marker.transform.position);

	}

	public override void InstallBindings()
	{
		BindInstallerIntarfaces();
	}

	private void BindInstallerIntarfaces()
	{
		BiindLocationInstaller();
		BindEnemyFactory();
	}

	private void BiindLocationInstaller()
	{
		Container.BindInterfacesTo<LocationInstaller>()
			.FromInstance(this)
			.AsSingle();
	}

	private void BindEnemyFactory()
	{
		Container.Bind<IEnemyFactory>()
			.To<EnemyFactory>()
			.FromComponentInNewPrefab(enemyFactoryPrefab)
			.AsSingle();
	}
}
