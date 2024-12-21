using Cache;
using Points;
using Sounds;
using UnityEngine;
using Zenject;

namespace Cartridges
{
	public class CartridgeUse : MonoCache
	{
		public CartridgeItem item;
		[Header("Effects")]
		[SerializeField] private ParticleSystem particleOnDestroy;
		[SerializeField] private float timeOfDestroy = 0.1f;

		[Inject] private ShopPoint shop;
		[Inject] private AudioManager audio;
		[Inject] private ItemsController itemsController;

		private const string candyAudio = GlobalStringsVars.AMMO_SOUND_NAME;
		/// <summary>
		/// use item baff
		/// </summary>
		public void UseItem()
		{
			if (item != null)
			{
				item.Use(shop);
				itemsController.CheckHandForChangeBulletUI(item.typeOfCartridge);
				Destroy(this.gameObject);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag != "Player")
				return;

			if (particleOnDestroy != null)
				particleOnDestroy.Play();

			UseItem();

			audio.PlaySound(candyAudio);

			if (particleOnDestroy != null)
			{
				var hitEffect = Instantiate(particleOnDestroy, transform.position, Quaternion.identity);

				Destroy(hitEffect.gameObject, 0.5f);
			}
			Destroy(this.gameObject, 0.5f);
		}
	}
}
