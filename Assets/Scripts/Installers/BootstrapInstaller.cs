using Languages;
using SaveSystem;
using Yandex;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		//Container.Bind<Language>().FromNew().AsSingle().NonLazy();
		BindGameReadyApi();
		BindProgress();
	}

	private void BindGameReadyApi()
	{
		Container.Bind<GameReadyApi>().FromNew().AsSingle().NonLazy();
	}
	private void BindProgress()
	{
		// FromNew() - �� ������������
		Container.Bind<Progress>().FromNew().AsSingle().NonLazy();
	}
}
