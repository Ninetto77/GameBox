using Attack.Base;
using Attack.Projectile;
using Items;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrenadeLauncher : AttackBehaviour
{
	[Header("Настройки")]
	public ProjectileWeaponItem weapon;
	public Transform FirePoint;
	public float TimeToAttack = 0.7f;

	[Header("Эффекты")]
	public ParticleSystem MuzzleEffect;

	private ProjectileAttack projectile;

	private float nextFireTime = 0;
	private bool canFire = true;
	private int bulletsPerMagazineDefault = 0;
	private int currentBulletsPerMagazine = 0;

	private Camera mainCamera;
	private ItemPickup item;
	private bool toolIsPicked;

	private void Start()
	{
		bulletsPerMagazineDefault = weapon.BulletsPerMagazine;
		currentBulletsPerMagazine = bulletsPerMagazineDefault;
		mainCamera = Camera.main;

		projectile = GetComponent<ProjectileAttack>();

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
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (Input.GetMouseButtonDown(0))
		{
			PerformAttack();
		}
		//if (Input.GetKeyDown(KeyCode.R) && canFire)
		//{
		//	StartCoroutine(Reload());
		//}
		if (Input.GetMouseButtonUp(0))
			EndAttack();

	}

	private void EndAttack()
	{
		OnAttackEnded?.Invoke();
	}

	public override void PerformAttack()
	{
		StartCoroutine(WaitToAttack());
		////if (canFire)
		////{

		////	if (currentBulletsPerMagazine > 0)
		////	{
		//		projectile.PerformAttack();
		//		OnAttackStarted?.Invoke();
		////	}
		////	else
		////	{
		////		StartCoroutine(Reload());
		////	}
		//PerformEffects();
		////}
	}

	private IEnumerator WaitToAttack()
	{
		OnAttackStarted?.Invoke();
		yield return new WaitForSeconds(TimeToAttack);
		projectile.PerformAttack();
		PerformEffects();
	}


	#region Частицы


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
