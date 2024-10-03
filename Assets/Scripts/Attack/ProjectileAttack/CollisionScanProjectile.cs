using UnityEngine;

namespace Attack.Projectile
{
	public class CollisionScanProjectile : Projectile
	{
		protected override void OnTargetCollision(Collision collision, IDamageable damageable)
		{
			damageable.ApplyDamage(Damage);
		}
	}
}