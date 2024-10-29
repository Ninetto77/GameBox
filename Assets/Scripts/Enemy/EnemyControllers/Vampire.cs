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
		}

		#region Атака
		public void Attack()
		{
			GetHeal();
			attack.PerformAttack();
			//player.ApplyDamage(Damage);
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