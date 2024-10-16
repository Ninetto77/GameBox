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

[System.Serializable]
public class SaveData
{
	public int Points;
	public int CountOfLevels;

}

