using UnityEngine;
namespace Enemy.States
{
    public class IdleState : EnemyState
    {
		private Rigidbody rb;
		public IdleState(IEnemy enemyController) : base(enemyController) { }

		public override void Update()
		{
			enemy.Animation.Walk(rb.velocity.magnitude);
		}

		public override void Enter()
		{
			rb = enemy.GetRigidBody();
			//rb.velocity = Vector3.zero;
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
