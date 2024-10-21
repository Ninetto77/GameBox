using UnityEngine;
using Zenject;
using Tasks;

public class TaskInstaller : MonoInstaller
{
	[SerializeField] private TaskManager TaskPrefab;
	public override void InstallBindings()
	{
		Container.Bind<TaskManager>().FromInstance(TaskPrefab).AsSingle().NonLazy();
		Container.QueueForInject(TaskPrefab);
	}
}