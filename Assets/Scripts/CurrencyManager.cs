using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{

    public static CurrencyManager Instance;

    [SerializeField]
    private int CurrentGold = 0;

    public UnityEvent GoldChanged;

    private string GoldSaveName = "GoldSave";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        SaveManager.Instance.SaveGameEvent.AddListener(SaveCurrency);
        SaveManager.Instance.LoadGameEvent.AddListener(LoadCurrency);
    }

    

    public int GetCurrentGold()
    {
        return CurrentGold;
    }

    public void AddGold(int goldAmount)
    {
        CurrentGold += goldAmount;
        GoldChanged.Invoke();
    }

    public bool HasEnoughGold(int targetAmount)
    {
        return CurrentGold >= targetAmount;
    }

    public bool SpendGold(int amount)
    {
        if (CurrentGold < amount) return false;

        CurrentGold -= amount;
        GoldChanged.Invoke();


        return true;

    }

    private void SetCurrentGold(int amount)
    {
        CurrentGold = amount;
        GoldChanged.Invoke();
    }

    private void SaveCurrency()
    {
        
        PlayerPrefs.SetInt(GoldSaveName, CurrentGold);
    }

    private void LoadCurrency()
    {
        if (!PlayerPrefs.HasKey(GoldSaveName)) return;

        SetCurrentGold(PlayerPrefs.GetInt(GoldSaveName));
        
    }
}
