using UnityEngine;

namespace Enemy.States
{
	public class AttackState : EnemyState
	{
		public AttackState(EnemyController enemyController) : base(enemyController) { }

		public override void Update()
		{ }

		/// <summary>
		/// поворот к игроку
		/// </summary>
		public void RotateToPlayer()
		{
			var direction = (enemy.TargetPosition - enemy.transform.position).normalized;
			var targetRotation = Quaternion.LookRotation(direction);
			enemy.transform.rotation = targetRotation;
		}

		public override void Enter()
		{
			base.Enter();
			RotateToPlayer();
			enemy.Animation.Attack(true);
		}

		public override void Exit()
		{
			enemy.Animation.Attack(false);
			base.Exit();
		}
	}
}