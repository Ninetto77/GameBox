using System.Collections;
using UnityEngine;

public partial class Weapon : MonoBehaviour, IWeapon
{
	public WeaponItem weapon;
	public Transform FirePoint;
	public float distanceToShoot = 100;

	private float nextFireTime = 0;
	private bool canFire = true;
	private int bulletsPerMagazineDefault = 0;
	private int currentBulletsPerMagazine = 0;
	
	private Camera mainCamera;
	private bool toolIsPicked;


	private void Start()
	{
		bulletsPerMagazineDefault = weapon.BulletsPerMagazine;
		currentBulletsPerMagazine = bulletsPerMagazineDefault;
		mainCamera = Camera.main;

		var temp = gameObject.GetComponent<ItemPickup>();
		toolIsPicked = temp.isPicked;
	}

	private void Update()
	{
		if (!toolIsPicked) return;

		if (Input.GetMouseButtonDown(0) && weapon.SingleFire)
		{
			Fire();
		}
		if (Input.GetMouseButton(0) && !weapon.SingleFire)
		{
			Fire();
		}
		if (Input.GetKeyDown(KeyCode.R) && canFire)
		{
			StartCoroutine(Reload());
		}
	}

	/// <summary>
	/// Стрельба
	/// </summary>
	private void Fire()
	{
		if (canFire)
		{
			if (Time.time > nextFireTime)
			{
				nextFireTime = Time.time + weapon.FireRate;

				if (currentBulletsPerMagazine > 0)
				{
					Vector3 firePointPointerPosition = mainCamera.transform.position + mainCamera.transform.forward * 100;
					RaycastHit hit;
					if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distanceToShoot))
					{
						firePointPointerPosition = hit.point;
					}

					FirePoint.LookAt(firePointPointerPosition);

					//Fire
					GameObject bulletObject = Instantiate(weapon.BulletPrefab, FirePoint.position, FirePoint.rotation);
					Bullet bullet = bulletObject.GetComponent<Bullet>();

					bullet.SetDamage(weapon.WeaponDamage);

					currentBulletsPerMagazine--;
				}
				else
				{
					StartCoroutine(Reload());
				}
			}
		}
	}

	/// <summary>
	/// Перезарядка
	/// </summary>
	/// <returns></returns>
	private IEnumerator Reload()
	{
		canFire = false;

		yield return new WaitForSeconds(weapon.TimeToReload);

		currentBulletsPerMagazine = bulletsPerMagazineDefault;

		canFire = true;
	}
}