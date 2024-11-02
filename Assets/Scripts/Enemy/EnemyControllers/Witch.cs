using UnityEngine;
using Zenject;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	public class Witch : EnemyController, IAttack
	{
		[Header("Атака ведьмы")]
		public FXType FxPrefab;
		public Transform FirePoint;


		[Inject] private ProjectContext projectContext;
		FXProvider fXProvider;

		private void Start()
		{
			fXProvider = projectContext.FXProvider;
		}

		public void Attack()
		{
			if (!canMove) return;
			PlayAttackSound();
			GetFireBall();
		}

		private void PlayAttackSound()
		{
			try
			{
				audioSource.PlayOneShot(attackSound);
			}
			catch (System.Exception)
			{
				Debug.Log($"no sound {attackSound.name}");
			}
		}

		private void GetFireBall()
		{
			var dir = new Vector3(FirePoint.position.x, FirePoint.position.y, FirePoint.position.z);

			if (FxPrefab == FXType.none) return;
			fXProvider.LoadFX(FxPrefab, dir, FirePoint.rotation);
		}

		private void OnDisable()
		{
			fXProvider.UnloadFX();
		}
	}
}
