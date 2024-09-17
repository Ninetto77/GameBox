namespace Enemy.States
{
	public class DamageState : EnemyState
	{
        public DamageState(EnemyController enemyController) : base(enemyController) { }

        public override void Update()
		{ }

		public override void Enter()
		{
			base.Enter();
			enemy.Animation.Damage();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}