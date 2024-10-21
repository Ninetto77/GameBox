using UnityEngine;
using Zenject;

public class ItemsInstaller : MonoInstaller
{
	[SerializeField] private ItemsController items;
	public override void InstallBindings()
	{
		Container.Bind<ItemsController>().FromInstance(items).AsSingle().NonLazy();
	}
}
