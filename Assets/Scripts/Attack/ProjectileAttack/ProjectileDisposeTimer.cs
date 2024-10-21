using Cache;
using UnityEngine;

namespace Attack.Projectile
{
	[RequireComponent(typeof(Projectile))]
	public class ProjectileDisposeTimer : MonoCache
	{
		[SerializeField, Min(0f)] private float _countdown = 15f;
		private Projectile _projectile;
		private float _elapsedTime;

		private void Awake()
		{
			_projectile = GetComponent<Projectile>();
		}

		public override void OnTick()
		{
			if (_projectile.IsProjectileDisposed)
				return;

			_elapsedTime += Time.deltaTime;


			if (_elapsedTime >= _countdown)
			{
				_projectile.DisposeProjectile();
			}
		}
	}
}
