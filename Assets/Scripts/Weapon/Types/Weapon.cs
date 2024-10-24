using System.Collections;
using UnityEngine;
using Attack.Base;
using Old;
using System;
using Items;
using Zenject;
using Sounds;

namespace Attack.Raycast
{
	public partial class Weapon : AttackBehaviour
	{
		[Header("Настройки")]
		public WeaponItem weapon;
		public Transform FirePoint;
		[Header("Эффекты")]
		public ParticleSystem MuzzleEffect;

		private float nextFireTime = 0;
		private bool canFire = true;

		private Camera mainCamera;
		private ItemPickup item;
		private bool toolIsPicked;

		private int RestCountOfBullets;
		private int curCountBulletsInPool;

		//		[Inject]private UIManager uiManager;
		//[Inject] private AudioManager audioManager;
		//public void Construct(AudioManager audio)
		//{
		//	this.audioManager = audio;
		//}

		private void Start()
		{
			RestCountOfBullets = weapon.CountOfBullets;
			curCountBulletsInPool = weapon.TotalBulletsInPool;

			mainCamera = Camera.main;

			ChangeIsPicked();
			item.OnChangeIsPicked += ChangeIsPicked;
		}

		private void ChangeIsPicked()
		{
			item = gameObject.GetComponent<ItemPickup>();
			toolIsPicked = item.IsPicked;
		}

		private void Update()
		{
			if (!toolIsPicked) return;

			if (Input.GetMouseButtonDown(0) && weapon.SingleFire)
			{
				PerformAttack();
			}
			if (Input.GetMouseButton(0) && !weapon.SingleFire)
			{
				PerformAttack();
			}
			if (Input.GetKeyDown(KeyCode.R) && canFire)
			{
				ReloudBullet();
			}
			if (Input.GetMouseButtonUp(0))
				EndAttack();

			if (weapon.UseZoom)
				if (Input.GetMouseButtonDown(1))
					OnZoomMouseClick?.Invoke();

		}

		private void EndAttack()
		{
			OnAttackEnded?.Invoke();
		}

		public override void PerformAttack()
		{
			if (canFire)
			{
				if (Time.time > nextFireTime)
				{
					nextFireTime = Time.time + weapon.FireRate;

					for (var i = 0; i < weapon.ShotCount; i++)
					{
						if (curCountBulletsInPool > 0)
						{
							OnAttackStarted?.Invoke();
							PerformRaycastCamera();
						}
					}
				}
			}
		}

		private void PerformRaycastCamera()
		{
			var direction = weapon.UseSpread ? mainCamera.transform.forward + CalculateSpread() : mainCamera.transform.forward;
			var ray = new Ray(mainCamera.transform.position, direction);

			FirePoint.LookAt(direction);

			if (Physics.Raycast(ray, out RaycastHit hitInfo, weapon.DistanceToShoot, weapon.Mask))
			{
				var hitCollider = hitInfo.collider;

				if (hitCollider.TryGetComponent(out IDamageable damageable))
				{
					damageable.ApplyDamage(weapon.WeaponDamage);
				}
				else
				{
					// On IDamageable is not found.
				}

				RestCountOfBullets--;
				curCountBulletsInPool--;

				SpawnParticleEffectOnHit(hitInfo);
			}

			PerformEffects();
		}

		/// <summary>
		/// Частицы при выстреле
		/// </summary>
		private void PerformEffects()
		{
			if (MuzzleEffect != null)
			{
				MuzzleEffect.Play();
			}
		}

		/// <summary>
		/// Частицы от удара
		/// </summary>
		/// <param name="hitInfo"></param>
		private void SpawnParticleEffectOnHit(RaycastHit hitInfo)
		{
			if (weapon.HitEffectPrefab != null)
			{
				var hitEffectRotation = Quaternion.LookRotation(hitInfo.normal);
				var hitEffect = Instantiate(weapon.HitEffectPrefab, hitInfo.point, hitEffectRotation);

				Destroy(hitEffect.gameObject, weapon.HitEffectDestroyDelay);
			}
		}

		private Vector3 CalculateSpread()
		{
			return new Vector3
			{
				x = UnityEngine.Random.Range(-weapon.SpreadFactor, weapon.SpreadFactor),
				y = UnityEngine.Random.Range(-weapon.SpreadFactor, weapon.SpreadFactor),
				z = UnityEngine.Random.Range(-weapon.SpreadFactor, weapon.SpreadFactor)
			};
		}

		private void ReloudBullet()
		{
			StartCoroutine(WaitToReloudBullet());

			//звук пyстого патрона
			if (RestCountOfBullets <= 0)
			{
				OnEmptyClip?.Invoke();
			}
		}

		/// <summary>
		/// Перезарядка
		/// </summary>
		/// <returns></returns>
		private IEnumerator WaitToReloudBullet()
		{
			if (RestCountOfBullets > 0)
			{
				canFire = false;

				OnReloud?.Invoke();

				yield return new WaitForSeconds(weapon.TimeToReload);
				int dif = weapon.TotalBulletsInPool - curCountBulletsInPool;
				if (dif > RestCountOfBullets)
				{
					dif = RestCountOfBullets;
				}
				curCountBulletsInPool += dif;
				RestCountOfBullets -= dif;
				
				canFire = true;
			}
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