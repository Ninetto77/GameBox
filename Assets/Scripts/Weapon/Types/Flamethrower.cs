using Attack.Base;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flamethrower : AttackBehaviour
{
	[Header("Настройки")]
	public WeaponItem weapon;
	public float TimeToStartAttack;
	

	[Header("Sounds")]
	[SerializeField] private AudioClip _emptyClipSound;
	[SerializeField] private AudioClip _shootSound;
	[SerializeField] private AudioClip _endedSound;

	[HideInInspector]
	public ItemPickup item;

	private AudioSource audioSource;
	private OverlapWithAttack overlap;
	private bool toolIsPicked;
	private bool isAttack;

	private void Start()
	{
		overlap = GetComponent<OverlapWithAttack>();
		isAttack = false;

		ChangeIsPicked();
		item.OnChangeIsPicked += ChangeIsPicked;

		audioSource = GetComponent<AudioSource>();
	}


	/// <summary>
	/// Изменить состояние "подобран"
	/// </summary>
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

		if (Input.GetMouseButton(GlobalStringsVars.FIRE))
		{
			StartAttack();
		}
		if (Input.GetMouseButtonUp(GlobalStringsVars.FIRE))
		{
			StopAttack();
		}
	}

	private void StopAttack()
	{
		if (isAttack == true)
		{
			isAttack = false;
			OnAttackEnded?.Invoke();
			SetSounds();
		}
	}

	private void StartAttack()
	{
		if (isAttack == false)
		{
			isAttack = true;
			OnAttackStarted?.Invoke();
			SetSounds();
		}
	}

	/// <summary>
	/// Воспроизвести звуки
	/// </summary>
	private void SetSounds()
	{
		if (isAttack)
		{
			audioSource.loop = true;
			audioSource.Play();
		}
		else
		{
			audioSource.Stop();
			audioSource.loop = false;
			audioSource.PlayOneShot(_endedSound);
		}
	}

	public override void PerformAttack()
	{
		return;
	}
}
