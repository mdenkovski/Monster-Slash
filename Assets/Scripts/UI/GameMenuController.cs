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
    [SerializeField]
    private GameUIWidget GameOverHUD;


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

    public void GoToGameOver()
    {
        HideAllMenus();
        GameOverHUD.EnableWidget();
    }

    private void HideAllMenus()
    {
        PauseMenu.DisableWidget();
        InventoryMenu.DisableWidget();
        ShopMenu.DisableWidget();
        GameHUD.DisableWidget();
        GameOverHUD.DisableWidget();
    }
}
