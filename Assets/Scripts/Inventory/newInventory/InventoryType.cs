using Items;

public static class InventoryType
{
	public static string GetInventoryName(ItemType type)
    {
		string MelleSlotname = GlobalStringsVars.MELLESLOT_NAME;
		string LightSlotname = GlobalStringsVars.LIGHTSLOT_NAME;
		string HeavySlotname = GlobalStringsVars.HEAVYSLOT_NAME;
		string ScientificSlotname = GlobalStringsVars.SCIENTIFICSLOT_NAME;

		return type switch
		{
			ItemType.melle => MelleSlotname,
			ItemType.lightWeapon => LightSlotname,
			ItemType.heavyWeapon => HeavySlotname,
			ItemType.scientificWeapon => ScientificSlotname,
			_ => "",
		};
	}
}
