using Attack.Base;
using UnityEngine;
using Zenject;
using Hints;

namespace WeaponObilities
{
    public class ShowEmptyClipHint : MonoBehaviour
    {
		private AttackBehaviour weapon;
		[Inject] private EmptyClipHint clipHint;

		private void OnEnable()
		{
			weapon = GetComponent<AttackBehaviour>();
			weapon.OnEmptyClip += UpdateUI;
		}

		private void UpdateUI()
		{
			clipHint.UpdateUI();
		}
	}
}
