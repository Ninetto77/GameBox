using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
	[SerializeField] private StateMashine stateMashine;
	public override void InstallBindings()
	{
		Container.Bind<StateMashine>().FromInstance(stateMashine).AsSingle().NonLazy();
	}
}