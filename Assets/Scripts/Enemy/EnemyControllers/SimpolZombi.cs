using UnityEngine;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	public class SimpolZombi : EnemyController, IAttack
	{
		public void Attack()
		{
			player.ApplyDamage(Damage);
		}
	}
}
