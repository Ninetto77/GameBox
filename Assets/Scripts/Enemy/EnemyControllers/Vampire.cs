using UnityEngine;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	public class Vampire : EnemyController, IAttack
	{
		[Header("Исцеление")]
		[Tooltip("Значения исцеления вампира")]
		public float healValue = 10;

		#region Атака
		public void Attack()
		{
			GetHeal();
			player.ApplyDamage(Damage);
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