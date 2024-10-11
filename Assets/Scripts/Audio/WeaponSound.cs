using Attack.Base;
using Attack.Raycast;
using System;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class WeaponSound : MonoBehaviour
{
	[Header("Sounds")]
	[SerializeField] private AudioClip _emptyClipSound;
	[SerializeField] private AudioClip _reloudSound;
	[SerializeField] private AudioClip _shootSound;
	[SerializeField] private AudioClip _endedSound;
	
	private AttackBehaviour weapon;
	private AudioSource audioSource;

	private void OnEnable()
	{
		weapon = GetComponent<AttackBehaviour>();
		weapon.OnAttackStarted += PlayShootSound;
		weapon.OnEmptyClip += PlayEmptyClipSound;
		weapon.OnReloud += PlayReloudSound;
		weapon.OnAttackEnded += PlayEndedSound;

		audioSource = GetComponent<AudioSource>();
	}


	private void OnDisable()
	{
		weapon.OnAttackStarted -= PlayShootSound;
		weapon.OnEmptyClip -= PlayEmptyClipSound;
		weapon.OnReloud -= PlayReloudSound;
		weapon.OnAttackEnded -= PlayEndedSound;
	}

	private void PlayShootSound()
	{
		try
		{
			audioSource.PlayOneShot(_shootSound);
		}
		catch (Exception)
		{
			Debug.LogWarning("Sound error");
		}
	}

	private void PlayEmptyClipSound()
	{
		try
		{
			audioSource.PlayOneShot(_emptyClipSound);
		}
		catch (Exception)
		{
			Debug.LogWarning("Sound error");
		}
	}

	private void PlayReloudSound()
	{
		try
		{
			audioSource.PlayOneShot(_reloudSound);
		}
		catch (Exception)
		{
			Debug.LogWarning("Sound error");
		}
	}

	private void PlayEndedSound()
	{
		if (_endedSound != null)
		{
			audioSource.PlayOneShot(_shootSound);
		}
	}

}
