using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Gameplay/new projectile weapon")]
	public class ProjectileWeaponItem : Equipable
	{
		[Header("Type of Cartridge")]
		[SerializeField] private TypeOfCartridge typeOfCartridge;

		[Header("Projectile Settings")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField, Min(0)] private float bulletSpeed = 100;
		[SerializeField] private Sprite aimIcon;

		[Header("Particle System")]
		[SerializeField] private ParticleSystem _hitEffectPrefab;
		[SerializeField, Min(0f)] private float _hitEffectDestroyDelay = 2f;

		[Header("Settings")]
		[SerializeField] private float timeToReload = 1.5f;

		[Header("Bullets")]
		[SerializeField] private int totalBulletsInPool = 30;


		public GameObject BulletPrefab => bulletPrefab;
		public Sprite AimIcon => aimIcon;

		public float BulletSpeed => bulletSpeed;

		public ParticleSystem HitEffectPrefab => _hitEffectPrefab;
		public float HitEffectDestroyDelay => _hitEffectDestroyDelay;

		public int TotalBulletsInPool => totalBulletsInPool;

		public float TimeToReload => timeToReload;
		public TypeOfCartridge TypeOfCartridge => typeOfCartridge;


		/// <summary>
		/// ������������� ����������
		/// </summary>
		public override void Use(EquipmentManager equipmentManager)
		{
			if (equipmentManager != null)
				equipmentManager.Equip(this);
			else Debug.Log("no equipmentManager in WeaponItem");
		}
	}
}