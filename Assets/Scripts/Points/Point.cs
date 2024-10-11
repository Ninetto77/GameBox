using System;

namespace Points
{
	[Serializable]
    public class Point
    {
        public int Value { get; set; }

		#region Конструктор
		public Point()
		{
			Value = 0;
		}

		public Point(int val)
		{
			Value = val;
		}
		#endregion

		public static Point operator +(Point a, Point b) => new Point(a.Value + b.Value);
		public static Point operator -(Point a, Point b) => new Point(a.Value - b.Value);
    }
}
