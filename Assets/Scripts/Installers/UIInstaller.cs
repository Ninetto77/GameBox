using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
	[SerializeField] private UIManager UIManagerPrefab;
	public override void InstallBindings()
	{
		Container.Bind<UIManager>().FromInstance(UIManagerPrefab).AsSingle().NonLazy();
	}
}