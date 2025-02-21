using UnityEngine;

namespace Enemy.States
{
	public class DeathState : EnemyState
	{
		public DeathState(IEnemy enemyController) : base(enemyController) { }
		private Vector3 direction = Vector3.zero;
		public override void Update()
		{ }

		public override void Enter()
		{
			base.Enter();
			RotateToPlayer();
			enemy.AnimationEnemy.Death();
		}


		/// <summary>
		/// Поворот к игроку
		/// </summary>
		public void RotateToPlayer()
		{
			direction = (enemy.TargetPosition - enemy.EnemyTransform.position).normalized;
			direction.y = 0;
			var targetRotation = Quaternion.LookRotation(direction);
			enemy.EnemyTransform.rotation = targetRotation;
		}
		public override void Exit()
		{
			base.Exit();
		}
	}
}