namespace Player.States
{
	public class StateMachine
	{
		public PlayerState CurrentState { get; private set; }

		public void Init(PlayerState newState)
		{
			CurrentState = newState;
			CurrentState.Enter();
		}
		public void ChangeState(PlayerState newState)
		{
			CurrentState.Exit();
			CurrentState = newState;
			//Debug.Log($"Current state is {CurrentState}");
			CurrentState.Enter();
		}
	}
}