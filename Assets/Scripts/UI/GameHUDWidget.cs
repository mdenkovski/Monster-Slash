using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHUDWidget : GameUIWidget
{
    [SerializeField]
    private TMP_Text GoldAmount;

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

}
