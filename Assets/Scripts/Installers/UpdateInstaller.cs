using Cache;
using UnityEngine;
using Zenject;

public class UpdateInstaller : MonoInstaller
{
	[SerializeField] private GlobalUpdate update;
	public override void InstallBindings()
	{
		Container.Bind<GlobalUpdate>().FromInstance(update).AsSingle().NonLazy();
	}
}