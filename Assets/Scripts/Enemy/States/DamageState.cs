using UnityEngine;

namespace Enemy.States
{
	public class DamageState : EnemyState
	{
        public DamageState(IEnemy enemyController) : base(enemyController) { }
		private Vector3 direction = Vector3.zero;

		public override void Update()
		{ }

		public override void Enter()
		{
			base.Enter();
			RotateToPlayer();
			enemy.Animation.Damage();
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