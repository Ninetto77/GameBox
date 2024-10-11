using System;
using UnityEngine;

namespace Attack.Base
{
	public abstract class AttackBehaviour : MonoBehaviour
	{
		public Action OnAttackStarted;
		public Action OnAttackEnded;
		public Action OnReloud;
		public Action OnZoomMouseClick;
		public Action OnEmptyClip;
		public Action OnEnemyHit;

		public abstract void PerformAttack();
	}
}
