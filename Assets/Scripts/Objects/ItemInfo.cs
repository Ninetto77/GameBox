using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Gameplay/New item")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

    public string Name => this._name;
    public Sprite Sprite => this._sprite;

    /// <summary>
    /// ������������� ��������
    /// </summary>
    public virtual void Use()
    {
        Debug.Log("Use " + name);
    }
}
