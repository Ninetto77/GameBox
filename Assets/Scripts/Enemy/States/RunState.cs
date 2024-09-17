using UnityEngine;

namespace Enemy.States
{
	public class RunState : EnemyState
	{
		private Vector3 direction = Vector3.zero;
		private float speed;
		private float angularSpeed;

		public RunState(EnemyController enemyController) : base(enemyController) 
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
			direction = (enemy.Player.transform.position - enemy.transform.position).normalized;
			var targetRotation = Quaternion.LookRotation(direction);
			enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, targetRotation,angularSpeed * Time.deltaTime);
		}

		/// <summary>
		/// Бег
		/// </summary>
		private void Run()
		{
			Rigidbody rb = enemy.GetRigidBody();
			rb.AddForce(enemy.transform.forward * Time.deltaTime * 20, ForceMode.Impulse);

			if (rb.velocity.magnitude > enemy.MaxSpeed)
				rb.velocity = Vector3.ClampMagnitude(rb.velocity, enemy.MaxSpeed);

			enemy.Animation.Walk(rb.velocity.magnitude);
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}