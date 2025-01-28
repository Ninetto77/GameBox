using Points;
using UnityEngine;
using Zenject;

public class PointsInstaller : MonoInstaller
{
	[SerializeField] private PointsLevel shop;
	public override void InstallBindings()
	{
		Container.Bind<PointsLevel>().FromInstance(shop).AsSingle().NonLazy();
	}
}
