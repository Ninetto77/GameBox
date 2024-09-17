using UnityEngine;

public class WeaponItem : ItemInfo
{
	[SerializeField] private GameObject _weaponPrefab;
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private float _weaponDamage= 15;
	[SerializeField] private bool singleFire = false;
	[SerializeField] private float fireRate = 0.1f;
	[SerializeField] private int bulletsPerMagazine = 30;
	[SerializeField] private float timeToReload = 1.5f;

	public GameObject WeaponPrefab => _weaponPrefab;
	public GameObject BulletPrefab => _bulletPrefab;
	public float WeaponDamage => _weaponDamage;
	public bool SingleFire => singleFire ;
	public float FireRate => fireRate;
	public int BulletsPerMagazine  => bulletsPerMagazine;
	public float TimeToReload => timeToReload;

	/// <summary>
	/// Использование экипировки
	/// </summary>
	public override void Use(EquipmentManager equipmentManager)
	{
		//if (equipmentManager != null)
			//equipmentManager.Equip(this);
		//else Debug.Log("no equipmentManager in Tool");
	}
}
