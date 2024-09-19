using UnityEngine;

[CreateAssetMenu(fileName = "ToolInfo", menuName = "Gameplay/New tool")]
public class Tool : Equipable
{
    [SerializeField] private int _speed;
    [SerializeField] private int _power;
    [SerializeField] private LayerMask layerMaskToHit;


	public int Speed => this._speed;
    public int Power => this._power;
    public LayerMask LayerMaskToHit => this.layerMaskToHit;

    /// <summary>
    /// Использование экипировки
    /// </summary>
    public override void Use(EquipmentManager equipmentManager)
    {
        if (equipmentManager != null)
		    equipmentManager.Equip(this);
		else Debug.Log("no equipmentManager in Tool");
	}
}


/// <summary>
/// Список, что экипировать
/// </summary>
/// <returns></returns>
public enum EquipmentsSlot
{
    // на этапе разбработки экипировать можно только одну руку
    hand, handweapon
}
