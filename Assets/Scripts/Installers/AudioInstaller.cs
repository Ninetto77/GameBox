using UnityEngine;
using Zenject;
using Sounds;

public class AudioInstaller : MonoInstaller
{
	[SerializeField] private AudioManager AudioManagerPrefab;
	public override void InstallBindings()
	{
		Container.Bind<AudioManager>().FromInstance(AudioManagerPrefab).AsSingle().NonLazy();
	}
}