using System;
using System.Linq;
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

    private string InventorySaveName = "InventorySave";

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

        SaveManager.Instance.SaveGameEvent.AddListener(SaveInventory);
        SaveManager.Instance.LoadGameEvent.AddListener(LoadInventory);

        //LoadInventory();
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

    public void AddItem(ItemScriptable item, bool equipped = false)
    {
        if (item == null) return;

        ItemScriptable itemClone = Instantiate(item);
        itemClone.Initialize(Controller);
        if (equipped)
        {
            itemClone.EquipItem(Controller);
        }
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
    private void SaveInventory()
    {
        InventorySave inventorySave = new InventorySave();
        List<ItemSave> ItemSaveList = GetItemList().Select(
            item => new ItemSave(item)).ToList();

        inventorySave.Items = ItemSaveList;
       

        string inventoryString = JsonUtility.ToJson(inventorySave);
        PlayerPrefs.SetString(InventorySaveName, inventoryString);
    }

    private void LoadInventory()
    {
        if (!PlayerPrefs.HasKey(InventorySaveName)) return;

        string loadedInventoryString = PlayerPrefs.GetString(InventorySaveName);
        InventorySave inventory = JsonUtility.FromJson<InventorySave>(loadedInventoryString);

        Items.Clear();
        foreach (ItemSave itemSaveData in inventory.Items)
        {
            ItemScriptable item = InventoryMasterList.Instance.GetItemReference(itemSaveData.Name);
            AddItem(item, itemSaveData.equipped);
        }
    }
}

[Serializable]
public class InventorySave
{
    public List<ItemSave> Items = new List<ItemSave>();
}
