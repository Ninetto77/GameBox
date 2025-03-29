using Enemy;
using Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerParticals : MonoBehaviour
{
	[Header("Ёффекты")]
	public ParticleSystem FireParticle;
	public ParticleSystem SparkParticle;

	public Flamethrower weapon;

	private bool isAttack = false;

	void Start()
    {
		weapon.OnAttackStarted += StartAttack;
		weapon.OnAttackEnded += StopAttack;
		FireParticle.Stop();
		SparkParticle.Stop();
	}

	private void StartAttack()
	{
		if (isAttack == false)
		{
			isAttack = true;
			FireParticle.Play();
			SparkParticle.Stop();
		}
	}

	private void StopAttack()
	{
		if (isAttack == true)
		{
			isAttack = false;
			FireParticle.Stop();
			SparkParticle.Play();
		}
	}

	public List<ParticleCollisionEvent> collisionEvents;

	private void OnParticleCollision(GameObject other)
	{
		if (other.layer == 8)
		{
			if (other == null) return;
			StartCoroutine(SetAttack(other));
		}
	}

	/// <summary>
	/// Ќаносить урон раз в секунду
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	private IEnumerator SetAttack(GameObject other)
	{
		while (isAttack)
		{
			if (other == null) yield break;

			var enemy = other.GetComponent<EnemyController>();

            if (enemy == null || enemy.gameObject == null)
            {
				yield break;
            }
            if (enemy != null)
			{
				enemy.ApplyDamage(weapon.weapon.WeaponDamage);
				yield return new WaitForSeconds(1.5f);
			}
		}
	}
	private void OnDisable()
	{
		weapon.OnAttackStarted -= StartAttack;
		weapon.OnAttackEnded -= StopAttack;
	}
}
