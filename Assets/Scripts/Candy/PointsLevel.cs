using Cache;
using System;

namespace Points
{
    public class PointsLevel : MonoCache
    {
		[ReadOnly]
		public Point curPoints;
		public Action<int> OnChangedPoints;
		public Action OnChangedCountCandies;

		[ReadOnly]
		public int countOfCandy = 0;

		private void Start()
		{
			//LoadPoints();
			curPoints = new Point();
			OnChangedPoints?.Invoke(curPoints.Value);
			countOfCandy = 0;
		}

		#region Действия конфет
		public void AddCandies()
		{
			countOfCandy++;
			OnChangedCountCandies?.Invoke();
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

		#region Сохранение и загрузка очков
		//public void SavePoints() => SaveSystemInFile.SavePoints(this);
		//public void LoadPoints()
		//{
		//	SavePoints points = SaveSystemInFile.LoadPoints();
		//	curPoints.Value = points.Points;
		//}

		//private void OnDisable()
		//{
		//	SavePoints();
		//}
		#endregion
	}


}
