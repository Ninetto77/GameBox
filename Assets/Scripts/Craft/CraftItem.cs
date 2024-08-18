using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Gameplay/New craft item")]
public class CraftItem : ScriptableObject
{
	public enum CraftType { Common, Tools }
	public CraftType craftType;
	public ItemInfo finalCraft;
	public int craftAmount;
	public int craftTime;

	public List<CraftResource> craftResources;

}
[System.Serializable]
public class CraftResource
{
	public ItemInfo craftObject;
	public int craftObjectAmount;
}