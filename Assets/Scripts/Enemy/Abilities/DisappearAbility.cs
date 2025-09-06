using Cache;
using UnityEngine;

namespace Enemy.Abilities
{
	public class DisappearAbility : MonoCache
	{
		[SerializeField] private bool changeEmmisionSwitcher = false;
		[SerializeField] Renderer enemyRenderer;
		[SerializeField] private float effectSpeed;

		private float currentValue = 1;
		private bool _activateSwitcher;

		public void Execute()
		{
			enemyRenderer.enabled = true;

			if (changeEmmisionSwitcher)
				enemyRenderer.material.SetFloat("_EmissionSwitcher", 1);
			enemyRenderer.material.SetFloat("_Dissolve", 1);

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
			if ( currentValue > 0)
			{
				currentValue -= effectSpeed * Time.deltaTime;
			}
			enemyRenderer.material.SetFloat("_Dissolve", currentValue);

			if ( currentValue <= 0 )
				_activateSwitcher = false;
		}
	}
}
