using Cache;
using UnityEngine;

namespace Disapear
{
	public class DisappearAbility : MonoCache
	{
		[SerializeField] Renderer enemyRenderer;
		[SerializeField] private float effectSpeed;

		private float currentValue = 1;
		private bool _activateSwitcher;

		public void Execute()
		{
			enemyRenderer.enabled = true;

			enemyRenderer.material.SetFloat("_EmissionSwitcher", 1);
			enemyRenderer.material.SetFloat("_Dissolve", 1);

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
		}
	}
}
