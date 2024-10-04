using Attack.Raycast;
using UnityEngine;
using Zenject;

namespace Weapon
{
	public class MelleWeapon : MonoBehaviour
	{
		private bool toolIsPicked;
		private OverlapWithAttack attack;

		private void Start()
		{
			var temp = gameObject.GetComponent<ItemPickup>();
			toolIsPicked = temp.isPicked;
			attack = GetComponent<OverlapWithAttack>();
		}

		private void Update()
		{
			if (!toolIsPicked) return;
			Debug.Log("update");

			if (Input.GetMouseButtonDown(0))
			{
				attack.PerformAttack();
				Debug.Log("PerformAttack");

			}
		}
	}
}
