using System;
using UnityEngine;

namespace Points
{
	public class CartridgeShop : MonoBehaviour
    {
        //[ReadOnly]
        //public Point curPoints;
        //public Action<int> OnChangedPoints;
        public Action OnAddHint;

		//событие при подборе пуль конкретного типа
		public Action<TypeOfCartridge, int> OnPickCartridge;
		//событие при изменении количества пуль конкретного типа
		public Action<TypeOfCartridge, int> OnChangeCartridge;

		[ReadOnly]
		public int LightCartridgeCount;
        [ReadOnly]
		public int HeavyCartridgeCount;
		[ReadOnly]
		public int OilCartridgeCount;

		private void Start()
		{
			LightCartridgeCount = 0;
			HeavyCartridgeCount = 0;
			OilCartridgeCount = 0;

			OnPickCartridge += PickCartridge;
			OnChangeCartridge += ChangeCartridge;
		}

		/// <summary>
		/// Изменение количества пуль
		/// </summary>
		/// <param name="type"></param>
		/// <param name="value"></param>
		private void ChangeCartridge(TypeOfCartridge type, int value)
		{
			switch (type)
			{
				case TypeOfCartridge.light:
					LightCartridgeCount = value;
					break;
				case TypeOfCartridge.heavy:
					HeavyCartridgeCount = value;
					break;
				case TypeOfCartridge.oil:
					OilCartridgeCount = value;
					break;
				default:
					Debug.Log("no patrons");
					break;
			}
		}



		/// <summary>
		/// Событие при подборе пуль
		/// </summary>
		/// <param name="type"></param>
		/// <param name="cartridge"></param>
		public void PickCartridge(TypeOfCartridge type , int cartridge)
		{
			switch (type)
			{
				case TypeOfCartridge.light:
					LightCartridgeCount += cartridge;
					OnChangeCartridge?.Invoke(type, LightCartridgeCount);
					break;
				case TypeOfCartridge.heavy:
					HeavyCartridgeCount += cartridge;
					OnChangeCartridge?.Invoke(type, HeavyCartridgeCount);
					break;
				case TypeOfCartridge.oil:
					OilCartridgeCount += cartridge;
					OnChangeCartridge?.Invoke(type, OilCartridgeCount);
					break;
				default:
					Debug.Log("no patrons");
					break;
			}
		}

		#region
		public void AddHeal()
		{
			OnAddHint?.Invoke();
		}
		#endregion

	}
}