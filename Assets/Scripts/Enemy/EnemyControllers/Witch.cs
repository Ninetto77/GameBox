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
		
		[Header("Атака на поле ведьмы")]
		public FXType FxMagicAreaPrefab;
		public Transform FirePointMagicArea;

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

		/// <summary>
		/// Магическая атака
		/// </summary>
		private void GetFireBall()
		{
			try
			{
				var dir = new Vector3(
					FirePoint.position.x, 
					FirePoint.position.y, 
					FirePoint.position.z);

			if (FxPrefab == FXType.none) return;
			fXProvider.LoadFX(FxPrefab, dir, FirePoint.rotation);
			}
			catch (System.Exception)
			{
				Debug.Log("No fireball attack");
			}
		}

		/// <summary>
		/// Атака на местность
		/// </summary>
		private void GetMagicArea()
		{
			try
			{
				var dir = new Vector3(
					FirePointMagicArea.position.x, 
					FirePointMagicArea.position.y, 
					FirePointMagicArea.position.z);

				if (FxPrefab == FXType.none) return;
				fXProvider.LoadFX(FxMagicAreaPrefab, dir, FirePointMagicArea.rotation);
			}
			catch (System.Exception)
			{
				Debug.Log("No area attack");
			}
		}

		private void OnDisable()
		{
			fXProvider.UnloadFX();
		}
	}
}
