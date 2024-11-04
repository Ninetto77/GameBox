using Cache;
using Points;
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

		/// <summary>
		/// use item baff
		/// </summary>
		public void UseItem()
		{
			if (item != null)
			{
				item.Use(shop);
				Destroy(this.gameObject);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (particleOnDestroy != null)
				particleOnDestroy.Play();

			UseItem();

			//shop.AddHeal();
			if (particleOnDestroy != null)
			{
				var hitEffect = Instantiate(particleOnDestroy, transform.position, Quaternion.identity);

				Destroy(hitEffect.gameObject, 0.5f);
			}
			Destroy(this.gameObject, 0.5f);
		}
	}
}
