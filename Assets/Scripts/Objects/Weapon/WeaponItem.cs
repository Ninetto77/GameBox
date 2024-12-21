using UnityEngine;


namespace Items
{
	[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Gameplay/New weapon")]
	public class WeaponItem : Equipable
	{
		[Header("Type of weapon")]
		[SerializeField] private TypeOfCartridge typeOfCartridge;

		[Header("Prefab")]
		[SerializeField] private Sprite aimIcon;

		[Header("Damage")]
		[SerializeField, Min(0)] private float weaponDamage = 15;

		[Header("Ray")]
		[SerializeField] private LayerMask layerMask;
		[SerializeField] private float distanceToShoot = Mathf.Infinity;
		[SerializeField] private int shotCount = 1;

		[Header("Particle System")]
		[SerializeField] private ParticleSystem _hitEffectPrefab;
		[SerializeField, Min(0f)] private float _hitEffectDestroyDelay = 2f;

		[Header("Spread")]
		[SerializeField] private bool useSpread = false;
		[SerializeField, Min(0)] private float spreadFactor = 1f;

		[Header("Zoom")]
		[SerializeField] private bool useZoom = false;

		[Header("Settings")]
		[SerializeField] private bool singleFire = false;
		[SerializeField] private float fireRate = 0.1f;
		[SerializeField] private float timeToReload = 1.5f;

		[Header("Bullets")]
		[SerializeField] private int totalBulletsInPool = 10;

	
		public TypeOfCartridge TypeOfCartridge => typeOfCartridge;
		
		public Sprite AimIcon => aimIcon;

		public float WeaponDamage => weaponDamage;


		public LayerMask Mask => layerMask;
		public float DistanceToShoot => distanceToShoot;
		public int ShotCount => shotCount;

		public ParticleSystem HitEffectPrefab  => _hitEffectPrefab;
		public float HitEffectDestroyDelay  => _hitEffectDestroyDelay;

		public bool UseSpread => useSpread;
		public float SpreadFactor => spreadFactor;		
		
		public bool UseZoom => useZoom;


		public bool SingleFire => singleFire;
		public float FireRate => fireRate;
		public int TotalBulletsInPool => totalBulletsInPool;
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
