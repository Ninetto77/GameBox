using Cache;
using Sounds;
using System;
using System.Collections;
using System.Data;
using UnityEngine;
using Zenject;


[RequireComponent(typeof(Health))]
public class DamagePlayer : MonoCache, IDamageable
{
	public Action OnPlayerDead;

	[HideInInspector]
	public Health health;

	[Inject]
	private AudioManager audioManager;
	[Inject]
	private UIManager uiManager;

	private const string deadName = GlobalStringsVars.DEATH_SOUND_NAME;
	private const string musicName = GlobalStringsVars.MAIN_MUSIC_NAME;

	private void Start()
	{
		health = GetComponent<Health>();
	}

	#region
	public void ApplyDamage(float damage)
	{
		health.TakeDamage(damage);

		if (health.GetCurrentHealth() <= 0)
		{
			StartCoroutine(DeadPlayer());
		}
		else
			uiManager.OnPlayerDamage?.Invoke();
	}
	#endregion
	private IEnumerator DeadPlayer()
	{
		yield return new WaitForEndOfFrame();
		GetComponent<InputKeys>().enabled = false;
		GetComponent<MouseLook>().enabled = false;

		audioManager.StopSound(musicName);
		audioManager.PlaySound(deadName);

		OnPlayerDead?.Invoke();
		uiManager.OnPlayerDead?.Invoke();
	}
}
