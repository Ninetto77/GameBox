using Hints;
using Tasks;
using UnityEngine;
using Zenject;

public class HintInstaller : MonoInstaller
{
	[SerializeField] private EmptyClipHint HintPrefab;
	public override void InstallBindings()
	{
		Container.Bind<EmptyClipHint>().FromInstance(HintPrefab).AsSingle().NonLazy();
		Container.QueueForInject(HintPrefab);
	}
}
