using UnityEngine;

namespace Enemy.States
{
	public class RunState : EnemyState
	{
		private Rigidbody rb;
		private Vector3 direction = Vector3.zero;
		private float speed;
		private float angularSpeed;

		public RunState(IEnemy enemyController) : base(enemyController) 
		{
			this.speed = enemy.Speed;
			angularSpeed = enemy.AngularSpeed;
		}
		public override void Update()
		{
			Run();
			RotateToPlayer();
		}

		/// <summary>
		/// Поворот к игроку
		/// </summary>
		public void RotateToPlayer()
		{
			direction = (enemy.TargetPosition - enemy.EnemyTransform.position).normalized;
			direction.y = 0;
			var targetRotation = Quaternion.LookRotation(direction);
			enemy.EnemyTransform.rotation = Quaternion.Lerp(enemy.EnemyTransform.rotation, targetRotation,angularSpeed * Time.deltaTime);
		}

		/// <summary>
		/// Бег
		/// </summary>
		private void Run()
		{
			rb.AddForce(enemy.EnemyTransform.forward * Time.deltaTime * speed, ForceMode.Impulse);

			if (rb.velocity.magnitude > enemy.MaxSpeed)
			{
				rb.velocity = Vector3.ClampMagnitude(rb.velocity, enemy.MaxSpeed);
				//Debug.Log(rb.velocity.magnitude);
			}

			enemy.Animation.Walk(rb.velocity.magnitude);
		}

		public override void Enter()
		{
			base.Enter();
			rb = enemy.GetRigidBody();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}