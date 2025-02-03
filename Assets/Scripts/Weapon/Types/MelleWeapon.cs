using Attack.Base;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack.Overlap
{
	public class MelleWeapon : AttackBehaviour
	{
		private bool toolIsPicked;
		private OverlapWithAttack attack;

		private void Start()
		{
			var temp = gameObject.GetComponent<ItemPickup>();
			toolIsPicked = temp.IsPicked;
			attack = GetComponent<OverlapWithAttack>();
		}
		public override void PerformAttack()
		{
			attack.PerformAttack();
		}

		private void Update()
		{
			if (!toolIsPicked) return;
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			if (Input.GetMouseButtonDown(0))
			{
				PerformAttack();
				OnAttackStarted?.Invoke();
			}
			if (Input.GetMouseButtonUp(0))
			{
				OnAttackEnded?.Invoke();
			}
		}
	}
}
