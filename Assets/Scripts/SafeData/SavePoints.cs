using Points;

[System.Serializable]
public class SavePoints
{
	public int Points;
	public int CountOfLevels;

	public SavePoints(ShopPoint shop)
	{
		Points = shop.curPoints.Value;
	}
}

