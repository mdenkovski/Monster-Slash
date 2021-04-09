using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopInventoryPanel : MonoBehaviour
{
    private PlayerController PlayerController;
    private RectTransform RectTransform;

    [SerializeField]
    private GameObject ShopInventorySlotPrefab;

    public ShopWidget ShopWidget;

    private ShopSlot SelectedItem;

    [SerializeField]
    private TMP_Text CurrentGold;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        PlayerController = FindObjectOfType<PlayerController>();
        WipeChildren();
    }

    private void OnEnable()
    {
        CurrencyManager.Instance.GoldChanged.AddListener(UpdateGoldUI);
        UpdateGoldUI();
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.GoldChanged.RemoveListener(UpdateGoldUI);

    }

    private void UpdateGoldUI()
    {
        CurrentGold.text = CurrencyManager.Instance.GetCurrentGold().ToString();
    }

    public void PopulatePanel(List<ItemScriptable> itemList)
    {
        WipeChildren();
        foreach (ItemScriptable item in itemList)
        {
            ShopSlot shopSlot = Instantiate(ShopInventorySlotPrefab, RectTransform).GetComponent<ShopSlot>();
            shopSlot.Initialize(item, this);
        }

    }

    private void WipeChildren()
    {
        foreach (RectTransform child in RectTransform)
        {
            Destroy(child.gameObject);
        }
        RectTransform.DetachChildren();
    }
    public void ItemSelected(GameObject selectedItem)
    {
        SelectedItem = selectedItem.GetComponent<ShopSlot>();
        foreach (RectTransform child in RectTransform)
        {
            if (child.gameObject != selectedItem)
            {
                child.GetComponent<ShopSlot>().Deselect();
            }
        }
    }

    public void PurchaseSelectedItem()
    {
        if (!SelectedItem) return;
        if (!CurrencyManager.Instance.SpendGold(SelectedItem.Item.Cost)) return;

        Debug.Log($"Purchased - {SelectedItem.Item.Name} for {SelectedItem.Item.Cost}");

        ShopWidget.RemoveItemFromInventory(SelectedItem.Item);
        PlayerController.Inventory.AddItem(SelectedItem.Item);
        Destroy(SelectedItem.gameObject);
        SelectedItem = null;
    }

}
