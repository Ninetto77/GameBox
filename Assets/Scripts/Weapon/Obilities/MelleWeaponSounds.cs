using Attack.Base;
using Attack.Overlap;
using System;
using UnityEngine;

[RequireComponent(typeof(MelleWeapon))]
[RequireComponent(typeof(AudioSource))]
public class MelleWeaponSounds : MonoBehaviour
{
	[Header("Sounds")]
	[SerializeField] private AudioClip _missSound;
	[SerializeField] private AudioClip _hitSound;

	private AttackBehaviour weapon;
	private AudioSource audioSource;

	private void OnEnable()
	{
		weapon = GetComponent<AttackBehaviour>();
		weapon.OnEnemyHit += PlayHitSound;
		weapon.OnMiss += PlayMissSound;

		audioSource = GetComponent<AudioSource>();
	}


	private void OnDisable()
	{
		weapon.OnEnemyHit -= PlayHitSound;
		weapon.OnMiss -= PlayMissSound;
	}

	private void PlayHitSound()
	{
		//Debug.Log("PlayShootSound");
		if (Time.timeScale == 0)
			return;
		try
		{
			audioSource.PlayOneShot(_hitSound);
		}
		catch (Exception)
		{
			Debug.LogWarning("Sound error");
		}
	}

	private void PlayMissSound()
	{
		//Debug.Log("PlayShootSound");
		if (Time.timeScale == 0)
			return;
		try
		{
			audioSource.PlayOneShot(_missSound);
		}
		catch (Exception)
		{
			Debug.LogWarning("Sound error");
		}
	}
}
