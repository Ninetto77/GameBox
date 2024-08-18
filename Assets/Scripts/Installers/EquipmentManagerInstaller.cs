using UnityEngine;
using Zenject;

public class EquipmentManagerInstaller : MonoInstaller
{
	[SerializeField] private EquipmentManager equipManager;
	public override void InstallBindings()
	{
		Container.Bind<EquipmentManager>().FromInstance(equipManager).AsSingle().NonLazy();
		Container.QueueForInject(equipManager);
	}
}