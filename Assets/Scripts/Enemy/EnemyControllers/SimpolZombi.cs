using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(OverlapWithAttack))]
	public class SimpolZombi : EnemyController, IAttack
	{
		private OverlapWithAttack attack;

		private void Start()
		{
			ChangeRandomTexture();

			attack = GetComponent<OverlapWithAttack>();
			attack.SetSearchMask(PlayerMask);
			attack.SetDamage(Damage);
		}
		public void Attack()
		{
			if (!canMove) return;

			PlayAttackSound();
			attack.PerformAttack();
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
		/// изменить при старте тектуру
		/// </summary>
		private void ChangeRandomTexture()
		{
			if (!gameObject.CompareTag("Zombi"))
				return;

			Renderer enemyRenderer = gameObject.GetComponentInChildren<Renderer>();
			int index = Random.Range(1, 5);

			if (enemyRenderer != null)
			{
				enemyRenderer.enabled = true;
				var material = Resources.Load($"Images/zombie{index}") as Texture2D;
				if (material != null)
				{
					enemyRenderer.material.SetTexture("_Albedo", material);
				}
			}
		}
	}
}
