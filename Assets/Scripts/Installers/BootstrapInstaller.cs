using Languages;
using SaveSystem;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		//Container.Bind<Language>().FromNew().AsSingle().NonLazy();
		BindProgress();
	}

	private void BindProgress()
	{
		// FromNew() - из конструктора
		Container.Bind<Progress>().FromNew().AsSingle().NonLazy();
	}
}
