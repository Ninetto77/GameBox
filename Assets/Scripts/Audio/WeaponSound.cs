using Attack.Base;
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
		Debug.Log("PlayShootSound");
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
		Debug.Log("PlayEmptyClipSound");

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
		Debug.Log("PlayReloudSound");

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
		Debug.Log("PlayEndedSound");

		if (_endedSound != null)
		{
			audioSource.PlayOneShot(_shootSound);
		}
	}

}
