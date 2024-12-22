using Cache;
using Enemy.States;
using System.Collections;
using UnityEngine;

namespace Enemy.Abilities
{
	public class BurnAbility : MonoCache
	{
		public ParticleSystem FirePartical;

		private bool IsFired = false;
		private EnemyController enemy;
		private float damage;

		private void Start()
		{
			FirePartical.Stop();
		}

		private void OnParticleCollision(GameObject other)
		{
			if (other.gameObject.CompareTag("Fire"))
			{
				StartCoroutine(StartBurn(other));
			}
		}

		private IEnumerator StartBurn(GameObject other)
		{
			if (!IsFired)
			{
				IsFired = true;
				FirePartical.Play();

				yield return new WaitForSeconds(2f);

				FirePartical.Stop();
				IsFired = false;
			}
		}
	}
}
