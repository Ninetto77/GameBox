using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack.Overlap
{
	public class MelleWeapon : MonoBehaviour
	{
		private bool toolIsPicked;
		private OverlapWithAttack attack;

		public Action OnAttackStarted;
		public Action OnAttackEnded;
		public Action OnEnemyHit;

		private void Start()
		{
			var temp = gameObject.GetComponent<ItemPickup>();
			toolIsPicked = temp.IsPicked;
			attack = GetComponent<OverlapWithAttack>();
		}

		private void Update()
		{
			if (!toolIsPicked) return;
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			if (Input.GetMouseButtonDown(0))
			{
				attack.PerformAttack();
				OnAttackStarted?.Invoke();
			}
			if (Input.GetMouseButtonUp(0))
			{
				OnAttackEnded?.Invoke();
			}
		}
	}
}
