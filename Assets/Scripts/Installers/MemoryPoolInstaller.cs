using Enemy.States;
using NTC.Pool;
using ObjectPoolZenject;
using UnityEngine;
using Zenject;

public class MemoryPoolInstaller : MonoInstaller
{
	[SerializeField] private PoolPreset simpolZombiPreset;
	[SerializeField] private PoolPreset hardZombiPreset;
	[SerializeField] private PoolPreset spiderPreset;

	[SerializeField] private PoolPreset skeletonPreset;
	[SerializeField] private PoolPreset manKillerPreset;

	[SerializeField] private PoolPreset witchPreset;
	[SerializeField] private PoolPreset mainWitchPreset;

	public override void InstallBindings()
	{
		BindPresets();
	}

	private void BindPresets()
	{
		Container.BindMemoryPool<SimpolZombi, SimpolZombiPool>()
			.WithInitialSize(simpolZombiPreset.Capacity) // Начальный размер пула
			.FromComponentInNewPrefab(simpolZombiPreset.Prefab) // Укажите префаб
			.UnderTransformGroup($"[Pool Object]: {simpolZombiPreset.Prefab.name}"); // Опционально: укажите родителя

		Container.BindMemoryPool<SimpolZombi, HardZombiPool>()
			.WithInitialSize(hardZombiPreset.Capacity)
			.FromComponentInNewPrefab(hardZombiPreset.Prefab)
			.UnderTransformGroup($"[Pool Object]: {hardZombiPreset.Prefab.name}");

		Container.BindMemoryPool<SmallEnemy, SpiderPool>()
		.WithInitialSize(spiderPreset.Capacity)
		.FromComponentInNewPrefab(spiderPreset.Prefab)
		.UnderTransformGroup($"[Pool Object]: {spiderPreset.Prefab.name}");

		Container.BindMemoryPool<SimpolZombi, SkeletonPool>()
		.WithInitialSize(skeletonPreset.Capacity)
		.FromComponentInNewPrefab(skeletonPreset.Prefab)
		.UnderTransformGroup($"[Pool Object]: {skeletonPreset.Prefab.name}");

		Container.BindMemoryPool<Witch, MainWitchPool>()
		.WithInitialSize(mainWitchPreset.Capacity)
		.FromComponentInNewPrefab(mainWitchPreset.Prefab)
		.UnderTransformGroup($"[Pool Object]: {mainWitchPreset.Prefab.name}");

		Container.BindMemoryPool<Witch, WitchPool>()
		.WithInitialSize(witchPreset.Capacity)
		.FromComponentInNewPrefab(witchPreset.Prefab)
		.UnderTransformGroup($"[Pool Object]: {witchPreset.Prefab.name}");

		Container.BindMemoryPool<SmallEnemy, ManKillerPool>()
		.WithInitialSize(manKillerPreset.Capacity)
		.FromComponentInNewPrefab(manKillerPreset.Prefab)
		.UnderTransformGroup($"[Pool Object]: {manKillerPreset.Prefab.name}");
	}
}
