using UnityEngine;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(OverlapWithAttack))]
	public class Vampire : EnemyController, IAttack
	{
		[Header("Исцеление")]
		[Tooltip("Значения исцеления вампира")]
		public float healValue = 10;

		private OverlapWithAttack attack;

		private void Start()
		{
			attack = GetComponent<OverlapWithAttack>();
			attack.SetSearchMask(PlayerMask);
			attack.SetDamage(Damage);
		}

		#region Атака
		public void Attack()
		{
			if (!canMove) return;

			GetHeal();
			PlayAttackSound();
			attack.PerformAttack();
			//player.ApplyDamage(Damage);
		}

		private void PlayAttackSound()
		{
			try
			{
				audioSource.PlayOneShot(attackSound);
			}
			catch (System.Exception)
			{
				Debug.Log($"no sound {attackSound.name}");
			}
		}
		#endregion

		#region Исцеление
		private void GetHeal()
		{
			health.TakeTreat(healValue);
		}
		#endregion
	}
}