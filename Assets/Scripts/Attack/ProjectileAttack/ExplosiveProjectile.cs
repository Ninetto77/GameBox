using System.Collections.Generic;
using UnityEngine;
using NTC.OverlapSugar;

namespace Attack.Projectile
{

	public class ExplosiveProjectile : Projectile
	{
		[Header("Explosion")]
		[SerializeField] private OverlapSettings _explosionOverlapSettings;

		private readonly List<IDamageable> _explosionOverlapResults = new(32);

		protected override void OnProjectileDispose()
		{
			PerformExplosion();
		}

		private void PerformExplosion()
		{
			if (_explosionOverlapSettings.TryFind(_explosionOverlapResults))
			{
				_explosionOverlapResults.ForEach(ApplyDamage);
			}
		}

		private void ApplyDamage(IDamageable damageable)
		{
			damageable.ApplyDamage(Damage);
		}

#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			_explosionOverlapSettings.TryDrawGizmos();
		}
#endif
	}
}

