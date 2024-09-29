using UnityEngine;


namespace Weapon
{
	[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Gameplay/New weapon")]
	public class WeaponItem : Equipable
	{
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private float weaponDamage = 15;
		[SerializeField] private bool singleFire = false;
		[SerializeField] private float fireRate = 0.1f;
		[SerializeField] private int bulletsPerMagazine = 30;
		[SerializeField] private float timeToReload = 1.5f;
		[SerializeField] private float distanceToShoot = 100;
		[SerializeField] private float distanceSpread = 1;
		[SerializeField] private Sprite aimIcon;

		public GameObject BulletPrefab => bulletPrefab;
		public float WeaponDamage => weaponDamage;
		public bool SingleFire => singleFire;
		public float FireRate => fireRate;
		public int BulletsPerMagazine => bulletsPerMagazine;
		public float TimeToReload => timeToReload;

		public float DistanceToShoot => distanceToShoot;
		public float DistanceSpread => distanceSpread;
		public Sprite AimIcon => aimIcon;


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
