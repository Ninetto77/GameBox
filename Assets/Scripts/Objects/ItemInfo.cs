using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Gameplay/New item")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    public string Name => this._name;
    public string Description => this._description;
    public Sprite Icon => this._icon;

    /// <summary>
    /// Использование предмета
    /// </summary>
    public virtual void Use(EquipmentManager manager)
    {
        Debug.Log("Use " + name);
    }
}
