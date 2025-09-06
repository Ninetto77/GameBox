using Languages;
using SaveSystem;
using Yandex;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		BindGameReadyApi();
		BindProgress();
		BindLanguage();
	}
	private void BindGameReadyApi()
	{
		Container.Bind<GameReadyApi>().FromNew().AsSingle().NonLazy();
	}
	private void BindProgress()
	{
		// FromNew() - из конструктора
		Container.Bind<Progress>().FromNew().AsSingle().NonLazy();
	}
	private void BindLanguage()
	{
		Container.Bind<Language>().FromNew().AsSingle().NonLazy();
	}
}
