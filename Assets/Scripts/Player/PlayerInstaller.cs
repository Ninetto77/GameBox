using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform StartPoint;

	public override void InstallBindings()
	{
		BindHero();
	}

	private void BindHero()
	{
		PlayerMoovement hero = Container.InstantiatePrefabForComponent<PlayerMoovement>(playerPrefab, StartPoint);

		Container.Bind(typeof(PlayerMoovement))
		  .FromInstance(hero)
		  .AsSingle()
		  .NonLazy();
	}
}