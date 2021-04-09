using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHUDWidget : GameUIWidget
{
    [SerializeField]
    private TMP_Text GoldAmount;
    [SerializeField]
    private TMP_Text CurrentWaveText;
    [SerializeField]
    private TMP_Text MaxWaveText;


    private int MaxWave = -1;


    private string MaxWaveSaveName = "WaveSave";

    private void Awake()
    {
        SaveManager.Instance.SaveGameEvent.AddListener(SaveMaxWave);
        SaveManager.Instance.LoadGameEvent.AddListener(LoadMaxWave);
    }

    private void OnEnable()
    {
        CurrencyManager.Instance.GoldChanged.AddListener(UpdateGoldAmount);
        UpdateGoldAmount();
    }


    private void UpdateGoldAmount()
    {
        GoldAmount.text = CurrencyManager.Instance.GetCurrentGold().ToString();
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.GoldChanged.RemoveListener(UpdateGoldAmount);
    }

    public void UpdateWaveInfo(int wave)
    {
        CurrentWaveText.text = wave.ToString();

        if (wave > MaxWave)
        {
            MaxWave = wave;
            MaxWaveText.text = MaxWave.ToString();
        }
    }

    private void SaveMaxWave()
    {
        PlayerPrefs.SetInt(MaxWaveSaveName, MaxWave);
    }

    private void LoadMaxWave()
    {
        if (!PlayerPrefs.HasKey(MaxWaveSaveName)) return;

        MaxWave = PlayerPrefs.GetInt(MaxWaveSaveName);
        MaxWaveText.text = MaxWave.ToString();
    }
}
