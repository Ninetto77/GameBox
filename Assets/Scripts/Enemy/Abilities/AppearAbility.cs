using Cache;
using UnityEngine;

namespace Enemy.Abilities
{
	public class AppearAbility : MonoCache
	{
		[SerializeField] private bool changeEmmisionSwitcher = false;
		[SerializeField] Renderer enemyRenderer;
		[SerializeField] private float effectSpeed;

		private float currentValue = 0;
		private bool _activateSwitcher;

		public void Execute()
		{
			enemyRenderer.enabled = true;

			if (changeEmmisionSwitcher) 
				enemyRenderer.material.SetFloat("_EmissionSwitcher", 0);
			enemyRenderer.material.SetFloat("_Dissolve", 0);

			currentValue = enemyRenderer.material.GetFloat("_Dissolve");
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
