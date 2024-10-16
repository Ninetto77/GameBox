using UnityEngine;

public class Equipable : ItemInfo
{
	[Header("Equip settings")]
	[SerializeField] private GameObject _prefab;
	[SerializeField] private EquipmentsSlot _equipmentSlot;
	[SerializeField] private FXType _fxType;

	public GameObject Prefab => _prefab;
	public EquipmentsSlot EquipmentSlot => this._equipmentSlot;
	public FXType FXType => this._fxType;

}
