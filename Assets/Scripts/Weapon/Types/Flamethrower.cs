using Attack.Base;
using Enemy;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flamethrower : AttackBehaviour
{
    public float Damage;
    public float TimeToStartAttack;
    public ParticleSystem FireParticle;
    public ParticleSystem SparkParticle;

	[Header("Sounds")]
	[SerializeField] private AudioClip _emptyClipSound;
	[SerializeField] private AudioClip _shootSound;
	[SerializeField] private AudioClip _endedSound;
	
	private AudioSource audioSource;


	private OverlapWithAttack overlap;
	private ItemPickup item;
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
			SparkParticle.Play();
			FireParticle.Stop();
			SetSounds();
		}
	}

	private void StartAttack()
	{
		if (isAttack  == false)
		{
			isAttack = true;

			FireParticle.Play();
			SparkParticle.Stop();
			StartCoroutine(SetAttack());
			//overlap.PerformAttack();

			OnAttackStarted?.Invoke();
			SetSounds();
		}
	}

	private IEnumerator SetAttack()
	{
		while(isAttack)
		{
			overlap.PerformAttack();
			yield return new WaitForSeconds(1f);
		}
	}

	public override void PerformAttack()
	{
		return;
	}
}
