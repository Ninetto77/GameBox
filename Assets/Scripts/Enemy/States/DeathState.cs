namespace Enemy.States
{
	public class DeathState : EnemyState
	{
		public DeathState(EnemyController enemyController) : base(enemyController) { }

        public override void Update()
		{ }

		public override void Enter()
		{
			base.Enter();
			enemy.Animation.Death();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}