using System;

namespace Old
{
	public interface IWeapon
	{
		Action OnAttackStarted { get; set; }
		Action OnAttackEnded{ get; set; }
		Action OnReloud{ get; set; }
		Action OnZoomMouseClick{ get; set; }
		Action OnEmptyClip{ get; set; }
		Action OnEnemyHit{ get; set; }
	}
}
