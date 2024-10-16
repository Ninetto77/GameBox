using UnityEngine;

[CreateAssetMenu(fileName = "BaffInfo", menuName = "Gameplay/New Baff")]

public class BaffItem : ItemInfo
{
	public float TreatPoints;
	public void Use(PlayerMoovement player)
	{
		if (player != null)
		{
			if (player.health != null)
			{
				player.health.TakeTreat(TreatPoints);
			}
		}
	}

}
