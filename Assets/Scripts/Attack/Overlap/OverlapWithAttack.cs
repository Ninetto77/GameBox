using NTC.OverlapSugar;
using System.Collections.Generic;
using UnityEngine;
using Attack.Base;
using GizmosType;
using Enemy;
using System;

public class OverlapWithAttack : AttackBehaviour
{
	public Action OnVisit;
	public Action OnMiss;

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

		// For single target.
		if (_overlapSettings.TryFind(out IWeaponVisitor visitor))
		{
			OnVisit?.Invoke();
		} 
		else
		{
			OnMiss?.Invoke();
		}

		// For many targets.
		//if (_overlapSettings.TryFind(_damageableResults))
		//{
		//	_damageableResults.ForEach(ApplyDamage);
		//}
	}

	private void ApplyDamage(IDamageable damageable)
	{
		damageable.ApplyDamage(_damage);
	}

	public void SetSearchMask(LayerMask newMask)
	{
		_overlapSettings.SetSearchMask(newMask);
	}

	public void SetDamage(float damage)
	{
		_damage = damage;
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
