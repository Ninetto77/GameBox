using UnityEngine;


namespace Items
{
	[CreateAssetMenu(fileName = "MelleWeaponInfo", menuName = "Gameplay/New melleweapon")]
	public class MelleWeaponItem : Equipable
	{
		[SerializeField] private float weaponDamage = 15;
		[SerializeField] private float velosity = 1f;

		public float WeaponDamage => weaponDamage;
		public float Velosity => velosity;


		/// <summary>
		/// Использование экипировки
		/// </summary>
		public override void Use(EquipmentManager equipmentManager)
		{
			if (equipmentManager != null)
				equipmentManager.Equip(this);
			else Debug.Log("no equipmentManager in WeaponItem");
		}
	}
}