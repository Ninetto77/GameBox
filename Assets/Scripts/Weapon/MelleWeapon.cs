using UnityEngine;
using Zenject;

namespace Weapon
{
	public class MelleWeapon : MonoBehaviour, IWeapon
	{
		public MelleWeaponItem item;
		[Inject] private PlayerMoovement player;
		void Fire()
		{

		}
	}
}
