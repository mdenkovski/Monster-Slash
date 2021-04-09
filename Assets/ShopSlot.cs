using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public ItemScriptable Item;

    private Button ItemButton;
    [SerializeField]
    private TMP_Text ItemNameText;
    [SerializeField]
    private TMP_Text ItemDescText;
    [SerializeField]
    private TMP_Text ItemCost;
    [SerializeField]
    private Image ItemImage;
    private ShopInventoryPanel ShopPanel;

    [SerializeField]
    private GameObject SelectedIcon;

    private void Awake()
    {
        ItemButton = GetComponent<Button>();
    }

    public void Initialize(ItemScriptable item, ShopInventoryPanel panel)
    {
        ShopPanel = panel;
        Item = item;
        ItemNameText.text = item.Name;
        ItemDescText.text = item.Description;
        ItemCost.text = item.Cost.ToString();
        ItemImage.sprite = item.ItemImage;
        SelectedIcon.SetActive(false);

        ItemButton.onClick.AddListener(SelectItem);
    }

    public void SelectItem()
    {
        SelectedIcon.SetActive(true);
        Debug.Log($"Selected - {Item.Name}");
        ShopPanel.ItemSelected(this.gameObject);
    }

    public void Deselect()
    {
        SelectedIcon.SetActive(false);

    }

    private void OnDisable()
    {
        ItemButton.onClick.RemoveListener(SelectItem);
    }
}
