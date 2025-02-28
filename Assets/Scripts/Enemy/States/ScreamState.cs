using UnityEngine;

namespace Enemy.States
{
    public class ScreamState : EnemyState
    {
		private float angularSpeed;

		public ScreamState(IEnemy enemyController) : base(enemyController) { }

		public override void Update() 
		{ RotateToPlayer(); }

		/// <summary>
		/// поворот к игроку
		/// </summary>
		public void RotateToPlayer()
		{
			var direction = (enemy.TargetPosition - enemy.EnemyTransform.position).normalized;
			var targetRotation = Quaternion.LookRotation(direction);
			enemy.EnemyTransform.rotation = targetRotation;
			//enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, targetRotation, angularSpeed * Time.deltaTime); // не работает
		}

		public override void Enter()
		{
			base.Enter();
			enemy.AnimationEnemy.Scream();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}

}
