using Attack.Base;
using Attack.Projectile;
using Items;
using Old;
using System;
using System.Collections;
using UnityEngine;

public class GrenadeLauncher : AttackBehaviour
{
  public ProjectileWeaponItem weapon;
	public Transform FirePoint;

	private ProjectileAttack projectile;

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

		projectile = GetComponent<ProjectileAttack>();
	}

	private void Update()
	{
		if (!toolIsPicked) return;

		if (Input.GetMouseButtonDown(0))
		{
			PerformAttack();
		}
		if (Input.GetKeyDown(KeyCode.R) && canFire)
		{
			StartCoroutine(Reload());
		}
		if (Input.GetMouseButtonUp(0))
			EndAttack();

	}

	private void EndAttack()
	{
		OnAttackEnded?.Invoke();
	}

	public override void PerformAttack()
	{
		//if (canFire)
		//{

		//	if (currentBulletsPerMagazine > 0)
		//	{
				projectile.PerformAttack();
				OnAttackStarted?.Invoke();
		//	}
		//	else
		//	{
		//		StartCoroutine(Reload());
		//	}
		//	PerformEffects();
		//}
	}


	#region Частицы

	/// <summary>
	/// Частицы при выстреле
	/// </summary>
	private void PerformEffects()
	{
		if (weapon.MuzzleEffect != null)
		{
			Debug.Log("MuzzleEffect");
			weapon.MuzzleEffect.Play();
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
