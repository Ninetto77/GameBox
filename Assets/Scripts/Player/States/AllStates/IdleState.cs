namespace Player.States
{
	public class IdleState : PlayerState
	{
		public IdleState(PlayerBrain enemyController) : base(enemyController) { }

		public override void Update()
		{ }

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

