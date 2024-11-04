using System;
using UnityEngine;
using Zenject.Asteroids;

namespace Points
{
    public class ShopPoint : MonoBehaviour
    {
        [ReadOnly]
        public Point curPoints;
        public Action<int> OnChangedPoints;
        public Action OnAddHint;

		public Action<TypeOfCartridge, int> OnPickCartridge;
		public Action<TypeOfCartridge, int> OnChangeCartridge;

		public Action<TypeOfCartridge, int> OnUseCartridge;

		[ReadOnly]
		public int LightCartridgeCount;
        [ReadOnly]
		public int HeavyCartridgeCount;
		[ReadOnly]
		public int OilCartridgeCount;

		private void Start()
		{
			LoadPoints();

			LightCartridgeCount = 0;
			HeavyCartridgeCount = 0;
			OilCartridgeCount = 0;
			
			//curPoints = new Point();
			OnChangedPoints?.Invoke(curPoints.Value);
			OnPickCartridge += PickCartridge;

			OnUseCartridge += UseCartridge;
		}

		private void UseCartridge(TypeOfCartridge type, int value)
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

		#region
		public void AddHeal()
        {
            OnAddHint?.Invoke();
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
		#endregion

		#region Действия с очками
		public void AddPoints(Point points)
        {
            curPoints += points;
            OnChangedPoints?.Invoke(curPoints.Value);
		}
        public void SpendPoints(Point points)
        {
            curPoints -= points;
			OnChangedPoints?.Invoke(curPoints.Value);
		}
        #endregion


        public void SavePoints() => SaveSystem.SavePoints(this);
        public void LoadPoints()
        {
            SavePoints points = SaveSystem.LoadPoints();
            curPoints.Value = points.Points;
        }

		private void OnDisable()
		{
            SavePoints();
		}
	}
}