using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemScriptable> Items = new List<ItemScriptable>();

    [SerializeField]
    private PlayerController Controller;


    public List<ItemScriptable> StartingInventory;

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
    }
    private void Start()
    {
        foreach (ItemScriptable item in StartingInventory)
        {
            AddItem(item);
        }
    }

    internal void DeactivateAllArmorExceptEquipped(ArmorScriptable armorRemaining)
    {
        foreach (ItemScriptable item in Items)
        {
            if (item is ArmorScriptable armor)
            {
                if (armor != armorRemaining)
                {
                    armor.Equipped = false;
                }
            }
        }
    }

    public void AddItem(ItemScriptable item)
    {
        if (item == null) return;

        ItemScriptable itemClone = Instantiate(item);
        itemClone.Initialize(Controller);
        Items.Add(itemClone);
    }

    public List<ItemScriptable> GetItemList()
    {
        return Items;
    }

    public int GetSize()
    {
        return Items.Count;
    }

    public void DeactivateAllWeaponsExceptEquipped(ItemScriptable weaponRemaining)
    {
        foreach (ItemScriptable item in Items)
        {
            if (item is WeaponScriptable weapon)
            {
                if (weapon != weaponRemaining)
                {
                    weapon.Equipped = false;
                }
            }
        }
    }
}
