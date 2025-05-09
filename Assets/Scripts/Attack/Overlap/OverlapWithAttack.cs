using NTC.OverlapSugar;
using System.Collections.Generic;
using UnityEngine;
using Attack.Base;
using GizmosType;

public class OverlapWithAttack : AttackBehaviour
{
	[SerializeField, Min(0f)] private float _damage = 10f;
	[SerializeField] private DrawGizmosType _drawGizmosType;
	[SerializeField] private OverlapSettings _overlapSettings;

	private readonly List<IDamageable> _damageableResults = new(32);

	[ContextMenu(nameof(PerformAttack))]
	public override void PerformAttack()
	{
		// For single target.
		if (_overlapSettings.TryFind(out IDamageable damageable))
		{
			ApplyDamage(damageable);
		}

		// For many targets.
		if (_overlapSettings.TryFind(_damageableResults))
		{
			_damageableResults.ForEach(ApplyDamage);
		}
	}

	private void ApplyDamage(IDamageable damageable)
	{
		damageable.ApplyDamage(_damage);
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (_drawGizmosType == DrawGizmosType.Always)
		{
			_overlapSettings.TryDrawGizmos();
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (_drawGizmosType == DrawGizmosType.OnSelected)
		{
			_overlapSettings.TryDrawGizmos();
		}
	}
#endif
}
