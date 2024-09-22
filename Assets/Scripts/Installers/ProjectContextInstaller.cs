using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<ProjectContext>().FromNew().AsSingle().NonLazy();
	}
}
