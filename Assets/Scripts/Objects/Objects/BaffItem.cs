using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BaffInfo", menuName = "Gameplay/New Baff")]

public class BaffItem : Equipable
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
				player.health.TakeTreat(TreatPoints);
			}
		}
	}
}
