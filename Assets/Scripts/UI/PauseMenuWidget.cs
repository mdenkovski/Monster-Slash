using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuWidget : GameUIWidget
{
    
    public void OnResume()
    {
        PlayerController controller = FindObjectOfType<PlayerController>();
        controller.ResumeGame();
    }

    public void OnReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    
}
