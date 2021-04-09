using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum ItemCategory
{
    None,
    Weapon,
    Armor
}

public abstract class ItemScriptable : ScriptableObject
{
    public string Name = "Item";
    public string Description = "Nothing";

    public Sprite ItemImage = null;

    public int Cost = 0;

    public bool Equipped
    {
        get => m_Equipped;
        set
        {
            m_Equipped = value;
            OnEquipStatusChange?.Invoke();
        }
    }
    private bool m_Equipped = false;
    public UnityEvent OnEquipStatusChange = new UnityEvent();

    public PlayerController Controller { get; private set; }

    public virtual void Initialize(PlayerController controller)
    {
        Controller = controller;
    }

    public abstract void EquipItem(PlayerController controller);

    
}

[Serializable]
public class ItemSave
{
    public string Name;
    public bool equipped;

    public ItemSave(ItemScriptable item)
    {
        Name = item.Name;
        equipped = item.Equipped;
    }
}
