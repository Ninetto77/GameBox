using UnityEngine;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent (typeof(OverlapWithAttack))]
	public class SimpolZombi : EnemyController, IAttack
	{
		private OverlapWithAttack attack;

		private void Start()
		{
			attack = GetComponent<OverlapWithAttack>();
			attack.SetSearchMask(PlayerMask);
		}
		public void Attack()
		{
			attack.PerformAttack();
		}
	}
}
