using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public UnityEvent SaveGameEvent;
    public UnityEvent LoadGameEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }


    }
    private void Start()
    {
        LoadGame();
    }
    public void SaveGame()
    {
        SaveGameEvent.Invoke();
    }

    public void LoadGame()
    {
        LoadGameEvent.Invoke();
    }

    private void OnDestroy()
    {
        SaveGameEvent.RemoveAllListeners();
        LoadGameEvent.RemoveAllListeners();
    }
}
