using UnityEngine;
using Attack.Base;
using Attack.Raycast;

namespace Attack.Projectile
{
	public class ProjectileAttack : AttackBehaviour
	{
		[SerializeField] private Transform _weaponMuzzle;
		[SerializeField] private Projectile _projectilePrefab;
		[SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
		[SerializeField, Min(0f)] private float _force = 10f;

		[ContextMenu(nameof(PerformAttack))]
		public override void PerformAttack()
		{
			var projectile = Instantiate(_projectilePrefab, _weaponMuzzle.position, _weaponMuzzle.rotation);

			var direction = Camera.main.transform.forward;

			projectile.Rigidbody.AddForce(_weaponMuzzle.forward * _force, _forceMode);
			//projectile.Rigidbody.AddForce(direction * _force, _forceMode);
		}
	}
}
