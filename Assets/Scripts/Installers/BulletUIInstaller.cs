using UnityEngine;
using Zenject;

public class BulletUIInstaller : MonoInstaller
{
	[SerializeField] private BulletUI BulletUIPrefab;
	public override void InstallBindings()
	{
		Container.Bind<BulletUI>().FromInstance(BulletUIPrefab).AsSingle().NonLazy();
		Container.QueueForInject(BulletUIPrefab);
	}
}