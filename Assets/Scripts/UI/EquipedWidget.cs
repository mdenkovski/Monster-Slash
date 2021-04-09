using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipedWidget : MonoBehaviour
{
    [SerializeField]
    private Image EnabledImage;

    private ItemScriptable Item;


    public void Initialize(ItemScriptable item)
    {
        Item = item;
        Item.OnEquipStatusChange.AddListener(OnEquipmentChanged);
        OnEquipmentChanged();
    }

    private void OnEquipmentChanged()
    {
        EnabledImage.gameObject.SetActive(Item.Equipped);

    }

    private void OnDisable()
    {
        Item?.OnEquipStatusChange.RemoveListener(OnEquipmentChanged);

    }

}
