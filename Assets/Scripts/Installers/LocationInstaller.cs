using Zenject;
using Enemy;

public class LocationInstaller : MonoInstaller, IInitializable
{
	public EnemyMarker[] enemyMarkers;

	public void Initialize()
	{
		var enemyFactory =  Container.Resolve<IEnemyFactory>();

		enemyFactory.Load();
		foreach (EnemyMarker marker in enemyMarkers)
			enemyFactory.Create(marker.EnemyType, marker.transform.position);
		
	}

	public override void InstallBindings()
	{
		BindInstallerIntarfaces();
	}

	private void BindInstallerIntarfaces()
	{
		Container.BindInterfacesTo<LocationInstaller>()
			.FromInstance(this)
			.AsSingle();
		
		Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
	}
}
