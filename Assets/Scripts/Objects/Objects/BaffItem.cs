using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BaffInfo", menuName = "Gameplay/New Baff")]

public class BaffItem : ItemInfo
{
	public float TreatPoints;
	[Inject] private PlayerMoovement player;
	public override void Use(EquipmentManager manager)
	{
		if (player != null)
		{
			Debug.Log(1);
			if (player.health != null)
			{
				Debug.Log(2);
				player.health.TakeTreat(TreatPoints);

			}
		}
	}
}
