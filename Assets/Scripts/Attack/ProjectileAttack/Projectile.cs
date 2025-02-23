using Unity.Burst.CompilerServices;
using UnityEngine;


namespace Attack.Projectile
{
	[SelectionBase]
	[RequireComponent(typeof(Rigidbody))]
	public abstract class Projectile : MonoBehaviour
	{
		[Header("Common")]
		[SerializeField, Min(0f)] private float _damage = 10f;
		[SerializeField] private ProjectileDisposeType _disposeType = ProjectileDisposeType.OnAnyCollision;
		[SerializeField] private LayerMask _targetMask;

		[Header("Rigidbody")]
		[SerializeField] private Rigidbody _projectileRigidbody;

		[Header("Effect On Destroy")]
		[SerializeField] private bool _spawnEffectOnDestroy = true;
		[SerializeField] private ParticleSystem _effectOnDestroyPrefab;
		[SerializeField, Min(0f)] private float _effectOnDestroyLifetime = 2f;

		public bool IsProjectileDisposed { get; private set; }
		public float Damage => _damage;
		public ProjectileDisposeType DisposeType => _disposeType;
		public LayerMask TargetMask => _targetMask;
		public Rigidbody Rigidbody => _projectileRigidbody;

		private void OnCollisionEnter(Collision collision)
		{
			if (IsProjectileDisposed)
				return;

			Debug.Log(collision.gameObject.name);
			if (collision.gameObject.TryGetComponent(out IDamageable damageable))
			{				
				if (TargetMask == (TargetMask | (1 << collision.gameObject.layer)))
				{
					OnTargetCollision(collision, damageable);

					if (_disposeType == ProjectileDisposeType.OnTargetCollision)
					{
						DisposeProjectile();
					}
				}
			}
			else
			{
				OnOtherCollision(collision);
			}

			OnAnyCollision(collision);

			if (_disposeType == ProjectileDisposeType.OnAnyCollision)
			{
				DisposeProjectile();
			}
		}

		public void DisposeProjectile()
		{
			OnProjectileDispose();

			SpawnEffectOnDestroy();

			Destroy(gameObject);

			IsProjectileDisposed = true;
		}

		private void SpawnEffectOnDestroy()
		{
			if (_spawnEffectOnDestroy == false)
				return;

			var effect = Instantiate(_effectOnDestroyPrefab, transform.position, _effectOnDestroyPrefab.transform.rotation);

			RaycastHit hit;
			if (Physics.Raycast(transform.position, -transform.up, out hit, 10f))
			{
				Vector3 normal = hit.normal;
				_effectOnDestroyPrefab.transform.rotation = Quaternion.Euler(normal);
			}

			Destroy(effect.gameObject, _effectOnDestroyLifetime);
		}

		protected virtual void OnProjectileDispose() { }
		protected virtual void OnAnyCollision(Collision collision) { }
		protected virtual void OnOtherCollision(Collision collision) { }
		protected virtual void OnTargetCollision(Collision collision, IDamageable damageable) { }
	}
}