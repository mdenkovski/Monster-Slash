using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUIWidget : GameUIWidget
{
    [SerializeField]
    private GameMenuController MenuController;

    [SerializeField]
    private TMP_Text WaveReachedText;

    private void OnEnable()
    {
        WaveReachedText.text = FindObjectOfType<MonsterSpawner>().GetCurrentWave().ToString();
        SaveManager.Instance.SaveGame();
    }


    public void OnPlayAgain()
    {
        FindObjectOfType<PlayerBehaviour>().RestartPlayer();
        FindObjectOfType<MonsterSpawner>().ResetGame();
        MenuController.ShowGameHUD();
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Mainmenu");
    }
}
