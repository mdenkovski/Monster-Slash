using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsAttacking;
    public bool IsRunning;

    private PlayerInput PlayerInput;

    private GameMenuController UIController;

    public PlayerInventory Inventory => InventoryComponent;
    [SerializeField] private PlayerInventory InventoryComponent;

    public GameplayStats Stats => GameplayStats;
    [SerializeField] private GameplayStats GameplayStats;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        UIController = FindObjectOfType<GameMenuController>();
        if (Inventory == null)
        {
            InventoryComponent = GetComponent<PlayerInventory>();
        }
        if (Stats == null)
        {
            GameplayStats = GetComponent<GameplayStats>();
        }
    }

    


    public void OnPause(InputValue value)
    {
        UIController.ShowPauseMenu();

        PlayerInput.SwitchCurrentActionMap("Pause");
        Debug.Log("Pause");
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnUnPause(InputValue value)
    {
        ResumeGame();
    }

    public void ResumeGame()
    {
        UIController.ShowGameHUD();
        PlayerInput.SwitchCurrentActionMap("HackAndSlash");
        Debug.Log("UnPause");
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnInventory()
    {
        UIController.ShowInventory();

        PlayerInput.SwitchCurrentActionMap("Pause");
        Debug.Log("Pause");
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
