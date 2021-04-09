using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWidget : GameUIWidget
{
    private InventoryPanelScript InventoryPanel;
    private PlayerController PlayerController;

    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        InventoryPanel = GetComponentInChildren<InventoryPanelScript>();
    }

    private void OnEnable()
    {
        if (!PlayerController || !PlayerController.Inventory) return;

        if (PlayerController.Inventory.GetSize() <= 0) return;


        InventoryPanel.PopulatePanel(PlayerController.Inventory.GetItemList());
    }
}
