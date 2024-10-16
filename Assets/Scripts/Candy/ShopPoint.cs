using System;
using UnityEngine;

namespace Points
{
    public class ShopPoint : MonoBehaviour
    {
        [ReadOnly]
        public Point curPoints;
        public Action<int> OnChangedPoints;
        public Action OnAddHint;

		private void Start()
		{
			LoadPoints();
			//curPoints = new Point();
			OnChangedPoints?.Invoke(curPoints.Value);
		}

		#region
        public void AddHeal()
        {
            OnAddHint?.Invoke();

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