using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private ItemScriptable Item;

    private Button ItemButton;
    [SerializeField]
    private TMP_Text ItemNameText;
    [SerializeField]
    private TMP_Text ItemDescText;
    [SerializeField]
    private Image IconImage;
    private EquipedWidget EquippedWidget;


    private void Awake()
    {
        ItemButton = GetComponent<Button>();
        EquippedWidget = GetComponentInChildren<EquipedWidget>();
    }

    public void Initialize(ItemScriptable item)
    {
        Item = item;
        ItemNameText.text = item.Name;
        ItemDescText.text = item.Description;
        IconImage.sprite = item.ItemImage;
        EquippedWidget.Initialize(item);

        ItemButton.onClick.AddListener(EquipItem);
    }
    
    public void EquipItem()
    {
        Item.EquipItem(Item.Controller);
    }

    private void OnDisable()
    {
        ItemButton.onClick.RemoveListener(EquipItem);
    }
}
