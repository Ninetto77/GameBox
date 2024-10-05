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
		reachDistance = 3f;
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

	/// <summary>
	/// Метод собирания предметов в инвентарь
	/// </summary>
	public void GatherResource()
	{
		Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
		RaycastHit hit;

		if (lastOutline != null)
			lastOutline.enabled = false;

		if (Physics.Raycast(ray, out hit, reachDistance))
		{
			ItemPickup item = hit.collider.gameObject.GetComponent<ItemPickup>();

			if (item != null)
			{
				lastOutline = item.outline;
				item.outline.enabled = true;
				if (Input.GetMouseButtonDown(0))
				{
					item.Interact();
				}
			}
			else if (lastOutline != null)
			{
				lastOutline.enabled = false;
				lastOutline = null;
			}
		}
	}

}
