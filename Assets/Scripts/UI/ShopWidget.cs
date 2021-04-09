using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShopWidget : GameUIWidget
{

    [SerializeField]
    private List<ItemScriptable> ShopInventory = new List<ItemScriptable>();

    private ShopInventoryPanel InventoryPanel;

    private string ShopInventorySaveName = "ShopInventorySave";

    private void Awake()
    {
        InventoryPanel = GetComponentInChildren<ShopInventoryPanel>();
        InventoryPanel.ShopWidget = this;
        SaveManager.Instance.SaveGameEvent.AddListener(SaveInventory);
        //SaveManager.Instance.LoadGameEvent.AddListener(LoadInventory);
        LoadInventory();
    }
    

    private void OnEnable()
    {
        
        InventoryPanel.PopulatePanel(ShopInventory);
    }

    public void RemoveItemFromInventory(ItemScriptable ItemToRemove)
    {
        ShopInventory.Remove(ItemToRemove);
    }


    private void SaveInventory()
    {
        InventorySave inventorySave = new InventorySave();
        List<ItemSave> ItemSaveList = ShopInventory.Select(
            item => new ItemSave(item)).ToList();

        inventorySave.Items = ItemSaveList;


        string inventoryString = JsonUtility.ToJson(inventorySave);
        PlayerPrefs.SetString(ShopInventorySaveName, inventoryString);
    }

    private void LoadInventory()
    {
        if (!PlayerPrefs.HasKey(ShopInventorySaveName)) return;

        string loadedInventoryString = PlayerPrefs.GetString(ShopInventorySaveName);
        InventorySave inventory = JsonUtility.FromJson<InventorySave>(loadedInventoryString);

        ShopInventory.Clear();
        foreach (ItemSave itemSaveData in inventory.Items)
        {
            ItemScriptable item = InventoryMasterList.Instance.GetItemReference(itemSaveData.Name);
            ShopInventory.Add(item);
        }
    }

}

[Serializable]
public class ShopInventorySave
{
    public List<ItemSave> Items = new List<ItemSave>();
}
