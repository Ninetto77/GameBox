//using ProjectContext = AdressableScripts.ProjectContext;

using UnityEngine;
using Attack.Base;
//using AdressableScripts;
using Zenject;

namespace Attack.Projectile
{
	public class ProjectileAttack : AttackBehaviour
	{
		[SerializeField] private Transform _firePoint;
		[SerializeField] private Projectile _projectilePrefab;
		[SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
		[SerializeField, Min(0f)] private float _force = 10f;

		[ContextMenu(nameof(PerformAttack))]
		public override void PerformAttack()
		{
			var projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);

			var direction = Camera.main.transform.forward;

			projectile.Rigidbody.AddForce(_firePoint.forward * _force, _forceMode);

			//projectile.Rigidbody.AddForce(direction * _force, _forceMode);
		}
	}
}
