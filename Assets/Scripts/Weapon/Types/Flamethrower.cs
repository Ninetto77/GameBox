using Attack.Base;
using Enemy;
using System;
using System.Collections;
using UnityEngine;

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

		ChangeIsPicked();
		item.OnChangeIsPicked += ChangeIsPicked;

		audioSource = GetComponent<AudioSource>();
	}

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

	private void ChangeIsPicked()
	{
		item = gameObject.GetComponent<ItemPickup>();
		toolIsPicked = item.IsPicked;
	}

	private void Update()
	{
		if (!toolIsPicked) return;

		if (Input.GetMouseButton(GlobalStringsVars.FIRE))
		{
			StartAttack();
		}
		if (Input.GetMouseButtonUp(GlobalStringsVars.FIRE))
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
			FireParticle.Play();
			SparkParticle.Stop();
			overlap.PerformAttack();

			isAttack = true;
			OnAttackStarted?.Invoke();
			SetSounds();
		}
	}

	public override void PerformAttack()
	{
		return;
	}
}
