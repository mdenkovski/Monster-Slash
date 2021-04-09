using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MonoBehaviour
{
    [SerializeField]
    private GameUIWidget PauseMenu;
    [SerializeField]
    private GameUIWidget InventoryMenu;
    [SerializeField]
    private GameUIWidget ShopMenu;
    [SerializeField]
    private GameUIWidget GameHUD;


    private void Awake()
    {
        ShowGameHUD();
    }

    public void ShowPauseMenu()
    {
        HideAllMenus();
        PauseMenu.EnableWidget();
    }

    public void ShowGameHUD()
    {
        HideAllMenus();
        GameHUD.EnableWidget();
    }

    public void GoToShop()
    {
        HideAllMenus();
        ShopMenu.EnableWidget();
    }

    public void ShowInventory()
    {
        HideAllMenus();
        InventoryMenu.EnableWidget();
    }

    private void HideAllMenus()
    {
        PauseMenu.DisableWidget();
        InventoryMenu.DisableWidget();
        ShopMenu.DisableWidget();
        GameHUD.DisableWidget();
    }
}
