using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelScript : MonoBehaviour
{
    private RectTransform RectTransform;

    [SerializeField]
    private GameObject InventorySlotPrefab;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        WipeChildren();
    }

    public void PopulatePanel(List<ItemScriptable> itemList)
    {
        WipeChildren();

        foreach (ItemScriptable item in itemList)
        {
            InventorySlot Icon = Instantiate(InventorySlotPrefab, RectTransform).GetComponent<InventorySlot>();
            Icon.Initialize(item);
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
}
