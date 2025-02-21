using Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy.Abilities
{
    public class AppearAbility : MonoCache
	{
		[SerializeField] Renderer enemyRenderer;
		[SerializeField] private float effectSpeed;

		private float currentValue = 0;
		private bool _activateSwitcher;

		public void Execute()
		{
			enemyRenderer.enabled = true;

			enemyRenderer.material.SetFloat("_EmissionSwitcher", 1);
			enemyRenderer.material.SetFloat("_Dissolve", 0);

			_activateSwitcher = true;
		}

		protected override void OnTick()
		{
			if (_activateSwitcher)
				Effect();
		}

		private void Effect()
		{
			if (currentValue <= 1)
			{
				currentValue += effectSpeed * Time.deltaTime;
			}
			enemyRenderer.material.SetFloat("_Dissolve", currentValue);


			if (currentValue > 1)
				_activateSwitcher = false;
		}
	}
}
