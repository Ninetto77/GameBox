using UnityEngine;
using Zenject;

public class CraftInstaller : MonoInstaller
{
	[SerializeField] private CraftManager craft;
	public override void InstallBindings()
	{
		Container.Bind<CraftManager>().FromInstance(craft).AsSingle().NonLazy();
	}
}