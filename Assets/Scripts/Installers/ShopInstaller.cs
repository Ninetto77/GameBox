using Points;
using UnityEngine;
using Zenject;

public class ShopInstaller : MonoInstaller
{
	[SerializeField] private ShopPoint shop;
	public override void InstallBindings()
	{
		Container.Bind<ShopPoint>().FromInstance(shop).AsSingle().NonLazy();
	}
}