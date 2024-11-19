using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Disapear
{
	public class DisappearAbility : MonoBehaviour
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

		private void Update()
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
