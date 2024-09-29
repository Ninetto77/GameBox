using System.Collections;
using UnityEngine;

namespace Weapon
{
	public partial class Weapon : MonoBehaviour, IWeapon
	{
		public WeaponItem weapon;
		public Transform FirePoint;

		private float nextFireTime = 0;
		private bool canFire = true;
		private int bulletsPerMagazineDefault = 0;
		private int currentBulletsPerMagazine = 0;

		private Camera mainCamera;
		private bool toolIsPicked;

		//[Inject] private ProjectContext projectContext;
		private FXProvider fXProvider;
		private FXType fxPrefab;


		private void Start()
		{
			bulletsPerMagazineDefault = weapon.BulletsPerMagazine;
			currentBulletsPerMagazine = bulletsPerMagazineDefault;
			mainCamera = Camera.main;

			var temp = gameObject.GetComponent<ItemPickup>();
			toolIsPicked = temp.isPicked;

			//fXProvider = projectContext.FXProvider;
			//fxPrefab = weapon.FXType;
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
						if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, weapon.DistanceToShoot))
						{
							firePointPointerPosition = hit.point;
						}

						FirePoint.LookAt(firePointPointerPosition);


						GameObject bulletObject = Instantiate(weapon.BulletPrefab, FirePoint.position, FirePoint.rotation);
						Bullet bullet = bulletObject.GetComponent<Bullet>();

						bullet.SetDamage(weapon.WeaponDamage);

						//GetFX(hit);

						currentBulletsPerMagazine--;
					}
					else
					{
						StartCoroutine(Reload());
					}
				}
			}
		}

		private Vector3 GetDirection()
		{
			return Vector3.forward;
		}

		#region
		private void GetFX(RaycastHit hit)
		{
			if (fxPrefab == FXType.none) return;
			//fXProvider.LoadFX(fxPrefab, transform.position, Quaternion.Euler(hit.normal));
			//fXProvider.LoadFX(fxPrefab, transform.position, Quaternion.identity, FirePoint);
			//GameObject game = task.Result;

			StartCoroutine(UnloadFX());
			//game.SetActive(false);
		}

		private IEnumerator UnloadFX()
		{
			yield return new WaitForSeconds(0.2f);
			Debug.Log("UnloadFX in weapon");

			fXProvider.UnloadFX();
		}
		#endregion

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
}