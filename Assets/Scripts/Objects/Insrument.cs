using UnityEngine;

[CreateAssetMenu(fileName = "IntrumentInfo", menuName ="Gameplay/New instrument")]
public class Insrument : ItemInfo
{
    [SerializeField] private int _speed;
    [SerializeField] private int _power;
    [SerializeField] private EquipmentsSlot _equipmentSlot;
    [SerializeField] private GameObject _prefab;


    public int Speed => this._speed;
    public int Power => this._power;
    public EquipmentsSlot EquipmentSlot => this._equipmentSlot;
    public GameObject Prefab => this._prefab;


    /// <summary>
    /// Использование экипировки
    /// </summary>
    public override void Use()
    {
        EquipmentManager.instance.Equip(this);
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
