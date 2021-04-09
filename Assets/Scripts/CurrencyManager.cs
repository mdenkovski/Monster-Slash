using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{

    public static CurrencyManager Instance;

    private int CurrentGold = 0;

    public UnityEvent GoldChanged;

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
}
