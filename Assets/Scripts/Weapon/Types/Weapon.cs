using System.Collections;
using UnityEngine;
using Attack.Base;
using Items;
using Zenject;
using Points;
using System;
using UnityEngine.EventSystems;

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

		public int CurCountBulletsInPool;
		private int commonCountOfBullets => GetCountsOfBullets();
		private int visibleBulletUI;

		[Inject] private CartridgeShop shop;
		[Inject] private BulletUI bulletUI;

		[Inject]
		public void Construct(CartridgeShop shop)
		{
			this.shop = shop;
		}

		/// <summary>
		/// Изменить общее количество пуль
		/// </summary>
		/// <param name="value"></param>
		private void ChangeTotalBulletsInThePool(int value)
		{
			var temp = GetCountsOfBullets() - value;
			shop.OnUseCartridge?.Invoke(weapon.TypeOfCartridge, (temp));
			bulletUI.OnChangeBullets?.Invoke(CurCountBulletsInPool, visibleBulletUI);
		}

		private void Start()
		{
			//RestCountOfBullets = weapon.CountOfBullets;
			//curCountBulletsInPool = weapon.TotalBulletsInPool;


			////изменение количества пуль в магазине
			////RestCountOfBullets = GetCountsOfBullets();
			//if (RestCountOfBullets > weapon.TotalBulletsInPool)
			//{
			//	curCountBulletsInPool = weapon.TotalBulletsInPool;
			//	shop.OnUseCartridge?.Invoke(weapon.TypeOfWeapon, RestCountOfBullets - weapon.TotalBulletsInPool);
			//}

			
			shop.OnChangeCartridge += ChangeVisibleBulletUI;

			CurCountBulletsInPool = 0;
			mainCamera = Camera.main;

			ChangeIsPicked();
			item.OnChangeIsPicked += ChangeIsPicked;
			visibleBulletUI = GetCountsOfBullets();

		}

		private void ChangeVisibleBulletUI(TypeOfCartridge cartridge, int arg2)
		{
			switch (cartridge)
			{
				case TypeOfCartridge.light:
					if (weapon.TypeOfCartridge == TypeOfCartridge.light)
					{
						visibleBulletUI = GetCountsOfBullets();
					}
					break;
				case TypeOfCartridge.heavy:
					if (weapon.TypeOfCartridge == TypeOfCartridge.heavy)
					{
						visibleBulletUI = GetCountsOfBullets();
					}
					break;
				case TypeOfCartridge.oil:
					break;
				case TypeOfCartridge.none:
					break;
				default:
					break;
			}
		}

		private void ChangeIsPicked()
		{
			item = gameObject.GetComponent<ItemPickup>();
			toolIsPicked = item.IsPicked;
		}

		private void Update()
		{
			if (!toolIsPicked) return;

			if (EventSystem.current.IsPointerOverGameObject())
				return;

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
				ReloadBullet();
			}

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
						if (CurCountBulletsInPool > 0)
						{
							if (i == 0)
								OnAttackStarted?.Invoke();
							PerformRaycastCamera();
						}
						else
						{
							OnEmptyClip?.Invoke();
						}
					}

				}
				if (Input.GetMouseButtonUp(0))
					EndAttack();
			}
		}

		private void PerformRaycastCamera()
		{
			var direction = weapon.UseSpread ? mainCamera.transform.forward + CalculateSpread() : mainCamera.transform.forward;
			var ray = new Ray(mainCamera.transform.position, direction);

			FirePoint.LookAt(direction);

			if (Physics.Raycast(ray, out RaycastHit hitInfo, weapon.DistanceToShoot, weapon.Mask, QueryTriggerInteraction.Ignore))
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
				SpawnParticleEffectOnHit(hitInfo);
			}

			CurCountBulletsInPool--;
			ChangeTotalBulletsInThePool(1);

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

		/// <summary>
		/// Вычисление разброса
		/// </summary>
		/// <returns></returns>
		private Vector3 CalculateSpread()
		{
			return new Vector3
			{
				x = UnityEngine.Random.Range(-weapon.SpreadFactor, weapon.SpreadFactor),
				y = UnityEngine.Random.Range(-weapon.SpreadFactor, weapon.SpreadFactor),
				z = UnityEngine.Random.Range(-weapon.SpreadFactor, weapon.SpreadFactor)
			};
		}

		/// <summary>
		/// Проверка на перезарядку
		/// </summary>
		/// <returns></returns>
		private void ReloadBullet()
		{
			StartCoroutine(WaitToReloadBullet());

			//звук пyстого патрона
			if (commonCountOfBullets <= 0)
			{
				OnEmptyClip?.Invoke();
			}
		}

		/// <summary>
		/// Перезарядка
		/// </summary>
		/// <returns></returns>
		private IEnumerator WaitToReloadBullet()
		{
			if (commonCountOfBullets > 0)
			{
				canFire = false;

				OnReloud?.Invoke();

				yield return new WaitForSeconds(weapon.TimeToReload);
				int dif = weapon.TotalBulletsInPool - CurCountBulletsInPool;
				if (dif > commonCountOfBullets)
				{
					dif = commonCountOfBullets;
				}

				CurCountBulletsInPool += dif;
				visibleBulletUI = visibleBulletUI - dif;
				bulletUI.OnChangeBullets?.Invoke(CurCountBulletsInPool, visibleBulletUI);
				
				//ChangeTotalBulletsInThePool(dif);
				//RestCountOfBullets -= dif;

				canFire = true;
			}
		}

		/// <summary>
		/// общее количество пуль
		/// </summary>
		/// <returns>общее количество пуль </returns>
		private int GetCountsOfBullets()
		{
			switch (weapon.TypeOfCartridge)
			{
				case TypeOfCartridge.light:
					return shop.LightCartridgeCount;
				case TypeOfCartridge.heavy:
					return shop.HeavyCartridgeCount;
				case TypeOfCartridge.oil:
					return shop.OilCartridgeCount;
				default:
					return 0;
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