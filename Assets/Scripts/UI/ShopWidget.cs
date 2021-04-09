using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWidget : GameUIWidget
{

    [SerializeField]
    private List<ItemScriptable> ShopInventory = new List<ItemScriptable>();

    private ShopInventoryPanel InventoryPanel;

    private void Awake()
    {
        InventoryPanel = GetComponentInChildren<ShopInventoryPanel>();
        InventoryPanel.ShopWidget = this;
    }

    private void OnEnable()
    {
        InventoryPanel.PopulatePanel(ShopInventory);
    }

    public void RemoveItemFromInventory(ItemScriptable ItemToRemove)
    {
        ShopInventory.Remove(ItemToRemove);
    }
}
