using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IntrumentInfo", menuName ="Gameplay/New instrument")]
public class Insrument : ItemInfo
{
    [SerializeField] private int _speed;
    [SerializeField] private int _power;
    [SerializeField] private EquipmentsSlot _equipmentSlot;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private FXType _fxType;
    [SerializeField] private LayerMask layerMaskToHit;


	public int Speed => this._speed;
    public int Power => this._power;
    public EquipmentsSlot EquipmentSlot => this._equipmentSlot;
    public GameObject Prefab => this._prefab;
    public FXType FXType => this._fxType;
    public LayerMask LayerMaskToHit => this.layerMaskToHit;

    /// <summary>
    /// Использование экипировки
    /// </summary>
    public override void Use(EquipmentManager equipmentManager)
    {
		equipmentManager.Equip(this);
    }
}


/// <summary>
/// Список, что экипировать
/// </summary>
/// <returns></returns>
public enum EquipmentsSlot
{
    // на этапе разбработки экипировать можно только одну руку
    hand
}
