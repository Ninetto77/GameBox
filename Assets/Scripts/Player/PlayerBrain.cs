using UnityEngine;
using Player.States;

public class PlayerBrain
{
	public float reachDistance;

	private OutlineObjects lastOutline;
	private Camera mainCamera;
	private StateMachine stateMachine;

    public PlayerBrain(float reachDistance)
    {
		mainCamera = Camera.main;
		this.reachDistance = reachDistance;
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
