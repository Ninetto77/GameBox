using UnityEngine;

namespace Player.States
{
	public class GatherState : PlayerState
	{
		private Camera mainCamera;
		private OutlineObjects lastOutline;
		private float reachDistance;


		public GatherState(PlayerBrain brain) : base(brain)
		{
			mainCamera = Camera.main;
			reachDistance = brain.reachDistance;
		}

		public override void Update()
		{
			GatherResource();
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
					if (Input.GetKeyDown(KeyCode.E))
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
