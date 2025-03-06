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

		[Header("Проджеклайл")]
		public FXType FxPrefab;
		[Inject] private ProjectContext projectContext;
		FXProvider fXProvider;

		private void Start()
		{
			fXProvider = projectContext.FXProvider;
		}

		[ContextMenu(nameof(PerformAttack))]
		public override void PerformAttack()
		{
			//if (FxPrefab == FXType.none) return;

			//var proj = fXProvider.LoadFX(FxPrefab, _firePoint.position, _firePoint.rotation).Result;
			//Projectile projectile = proj.GetComponent<Projectile>();
			//projectile.Rigidbody.AddForce(_firePoint.forward * _force, _forceMode);


			var projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);

			var direction = Camera.main.transform.forward;

			projectile.Rigidbody.AddForce(_firePoint.forward * _force, _forceMode);
			//projectile.Rigidbody.AddForce(direction * _force, _forceMode);
		}
	}
}
