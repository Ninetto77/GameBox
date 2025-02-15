using Attack.Base;
using Attack.Projectile;
using Items;
using Points;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

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

	private Camera mainCamera;
	private ItemPickup item;
	private bool toolIsPicked;

	//все связвнное с пулями и их отображением на UI
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

	private void Start()
	{
		mainCamera = Camera.main;

		projectile = GetComponent<ProjectileAttack>();

		ChangeIsPicked();
		item.OnChangeIsPicked += ChangeIsPicked;

		//пули
		shop.OnChangeCartridge += ChangeVisibleBulletUI;
		CurCountBulletsInPool = 0;
		visibleBulletUI = GetCountsOfBullets();

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
		if (Input.GetKeyDown(KeyCode.R) && canFire)
		{
			ReloadBullet();
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
		if (canFire)
		{
			if (CurCountBulletsInPool > 0)
				StartCoroutine(WaitToAttack());
			else
			{
				OnEmptyClip?.Invoke();
			}
		}
	}

	private IEnumerator WaitToAttack()
	{
		OnAttackStarted?.Invoke();
		yield return new WaitForSeconds(TimeToAttack);
		projectile.PerformAttack();
		PerformEffects();

		CurCountBulletsInPool--;
		ChangeTotalBulletsInThePool(1);
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

	# region Перезарядка

	/// <summary>
	/// Начать перезарядку
	/// </summary>
	/// <returns></returns>
	private void ReloadBullet()
	{
		//звук пyстого патрона
		if (commonCountOfBullets <= 0)
		{
			OnEmptyClip?.Invoke();
		}
		else
		{
			StartCoroutine(Reload());

		}
	}

	/// <summary>
	/// Перезарядка
	/// </summary>
	/// <returns></returns>
	private IEnumerator Reload()
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

		canFire = true;
	}
	#endregion

	#region Пули и пули в UI
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
	#endregion

}
