using Points;
using UnityEngine;
using Zenject;

public class CartridgeShopInstaller : MonoInstaller
{
	[SerializeField] private CartridgeShop shop;
	public override void InstallBindings()
	{
		Container.Bind<CartridgeShop>().FromInstance(shop).AsSingle().NonLazy();
	}
}