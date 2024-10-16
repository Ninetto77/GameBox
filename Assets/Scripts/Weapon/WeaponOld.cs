using System.Collections;
using UnityEngine;
using Items;

namespace Old
{
	public class WeaponOld : MonoBehaviour
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
			//bulletsPerMagazineDefault = weapon.BulletsPerMagazine;
			currentBulletsPerMagazine = bulletsPerMagazineDefault;
			mainCamera = Camera.main;

			var temp = gameObject.GetComponent<ItemPickup>();
			toolIsPicked = temp.IsPicked;

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
		/// ��������
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


						//GameObject bulletObject = Instantiate(weapon.BulletPrefab, FirePoint.position, FirePoint.rotation);
						//BulletRB bullet = bulletObject.GetComponent<BulletRB>();

					//	bullet.SetDamage(weapon.WeaponDamage);

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

		#region �������
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
		/// �����������
		/// </summary>
		/// <returns></returns>
		private IEnumerator Reload()
		{
			canFire = false;

			yield return new WaitForSeconds(weapon.TimeToReload);

			currentBulletsPerMagazine = bulletsPerMagazineDefault;

			canFire = true;
		}


#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			var ray = new Ray(transform.position, transform.forward);
			DrawRaycast(ray);
		}

		private void DrawRaycast(Ray ray)
		{
			if (Physics.Raycast(ray, out var hitInfo, weapon.DistanceToShoot, weapon.Mask))
			{
				DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
			}
			else
			{
				var hitPosition = ray.origin + ray.direction * weapon.DistanceToShoot;

				DrawRay(ray, hitPosition, weapon.DistanceToShoot, Color.green);
			}
		}

		private static void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
		{
			const float hitPointRadius = 0.15f;
			Debug.DrawRay(ray.origin, ray.direction * distance, color);

			Gizmos.color = color;
			Gizmos.DrawSphere(hitPosition, hitPointRadius);
		}
#endif
	}
}
