using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Gameplay/new projectile weapon")]
	public class ProjectileWeaponItem : Equipable
	{

		[Header("Projectile Settings")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField, Min(0)] private float bulletSpeed = 100;
		[SerializeField] private Sprite aimIcon;

		[Header("Particle System")]
		[SerializeField] private ParticleSystem _muzzleEffect;
		[SerializeField] private ParticleSystem _hitEffectPrefab;
		[SerializeField, Min(0f)] private float _hitEffectDestroyDelay = 2f;

		[Header("Settings")]
		[SerializeField] private int bulletsPerMagazine = 30;
		[SerializeField] private float timeToReload = 1.5f;

		public GameObject BulletPrefab => bulletPrefab;
		public Sprite AimIcon => aimIcon;

		public float BulletSpeed => bulletSpeed;

		public ParticleSystem MuzzleEffect => _muzzleEffect;
		public ParticleSystem HitEffectPrefab => _hitEffectPrefab;
		public float HitEffectDestroyDelay => _hitEffectDestroyDelay;

		public int BulletsPerMagazine => bulletsPerMagazine;
		public float TimeToReload => timeToReload;

		/// <summary>
		/// Использование экипировки
		/// </summary>
		public override void Use(EquipmentManager equipmentManager)
		{
			if (equipmentManager != null)
				equipmentManager.Equip(this);
			else Debug.Log("no equipmentManager in WeaponItem");
		}
	}
}