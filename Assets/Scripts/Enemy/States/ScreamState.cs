using UnityEngine;

namespace Enemy.States
{
    public class ScreamState : EnemyState
    {
		private float angularSpeed;

		public ScreamState(EnemyController enemyController) : base(enemyController) { }

		public override void Update() 
		{ RotateToPlayer(); }

		/// <summary>
		/// поворот к игроку
		/// </summary>
		public void RotateToPlayer()
		{
			var direction = (enemy.Player.transform.position - enemy.transform.position).normalized;
			var targetRotation = Quaternion.LookRotation(direction);
			enemy.transform.rotation = targetRotation;
			//enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, targetRotation, angularSpeed * Time.deltaTime); // не работает
		}

		public override void Enter()
		{
			base.Enter();
			enemy.Animation.Scream();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}

}
