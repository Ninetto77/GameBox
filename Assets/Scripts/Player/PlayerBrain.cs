using UnityEngine;
using Player.States;

public class PlayerBrain
{
	public float reachDistance = 1.5f;

	private OutlineObjects lastOutline;
	private Camera mainCamera;
	private StateMachine stateMachine;

    public PlayerBrain()
    {
		mainCamera = Camera.main;
		reachDistance = 3.5f;
		stateMachine = new StateMachine();
		stateMachine.Init(FactoryState.GetStatePlayer(StatesEnum.gather, this));
	}

	public void Update()
	{
		if (stateMachine.CurrentState is IdleState)
			return;
		else
		if (stateMachine.CurrentState is ShootState)
			return;
		else
		if (stateMachine.CurrentState is GatherState)
			stateMachine.CurrentState.Update();
	}

}
