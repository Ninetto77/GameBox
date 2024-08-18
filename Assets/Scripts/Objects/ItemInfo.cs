using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Gameplay/New item")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    public string Name => this._name;
    public Sprite Icon => this._icon;

    /// <summary>
    /// Использование предмета
    /// </summary>
    public virtual void Use(EquipmentManager manager)
    {
        Debug.Log("Use " + name);
    }
}
