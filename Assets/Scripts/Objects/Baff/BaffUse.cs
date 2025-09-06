using Zenject;
using UnityEngine;
using Cache;
using Points;
using Sounds;

using NTC.Pool;
using static NTC.Pool.NightPool;

public class BaffUse : MonoCache
{
	public BaffItem item;
	[Header("Effects")]
	[SerializeField] private ParticleSystem particleOnDestroy;
	[SerializeField] private float timeOfDestroy = 0.1f;

	[Inject] private PlayerMoovement player;
	[Inject] private CartridgeShop shop;
	[Inject] private AudioManager audioManager;

	private const string candyAudio = GlobalStringsVars.CANDY_SOUND_NAME;

	/// <summary>
	/// use item baff
	/// </summary>
	public void UseItem()
	{
		if (item != null)
		{
			item.Use(player);
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (particleOnDestroy != null)
			particleOnDestroy.Play();

		UseItem();

		shop.AddHeal();
		audioManager.PlaySound(candyAudio);

		if (particleOnDestroy != null)
		{
			Spawn(particleOnDestroy, transform.position, Quaternion.identity)
			.DespawnOnComplete();
		}
		Destroy(this.gameObject, 0.5f);
	}
}
