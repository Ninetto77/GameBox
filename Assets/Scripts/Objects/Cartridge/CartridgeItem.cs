using Points;
using UnityEngine;

namespace Cartridges
{
	[CreateAssetMenu(fileName = "CartridgeInfo", menuName = "Gameplay/New Cartridge")]

	public class CartridgeItem : ItemInfo
	{
		[Header("Settings")]
		public TypeOfCartridge typeOfCartridge;
		public int CountOfCartridge;

		public void Use(CartridgeShop shop)
		{
			if (shop != null)
				shop.OnPickCartridge?.Invoke(typeOfCartridge, CountOfCartridge);
			else
				Debug.Log("no shop.OnAddCartridge");
		}
	}
}