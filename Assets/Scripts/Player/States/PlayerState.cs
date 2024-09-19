namespace Player.States
{
	public abstract class PlayerState : IPlayerState
	{
		public bool isFinish;
		protected PlayerBrain player;

		public PlayerState()
		{
		}
		public PlayerState(PlayerBrain playerBrain)
		{
			player = playerBrain;
		}

		/// <summary>
		/// call each frame
		/// </summary>
		public virtual void Update() { }
		/// <summary>
		/// enter to the state
		/// </summary>
		public virtual void Enter()
		{
			isFinish = false;
		}
		/// <summary>
		/// exit from the state
		/// </summary>
		public virtual void Exit()
		{
			isFinish = true;
		}
	}
}
