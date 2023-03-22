using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public Inventory inventory { get; private set; }
    public Transform canvas;
    public Transform InventoryPanel;
    private List<UIInventorySlot> uiInventorySlot = new();
    public UIInventory(Inventory inventory, Transform InventoryPanel)
    {
        this.inventory = inventory;
        this.InventoryPanel = InventoryPanel;
        for (var i = 0; i < InventoryPanel.childCount; i++)
        {
            uiInventorySlot.Add(InventoryPanel.GetChild(i).GetComponent<UIInventorySlot>());
            uiInventorySlot[i].SetUIItem(uiInventorySlot[i].GetComponentInChildren<UIItem>());
            //Refresh();
            var slot = inventory.slots[i];
            var uiSlot = uiInventorySlot[i];
            uiSlot.SetSlot(slot);
            uiSlot.SetInventory(this);
            uiSlot.Refresh();
        }
    }
    public void Refresh()
    {
        for (var i = 0; i < inventory.capacity; i++)
        {
            var slot = inventory.slots[i];
            var uiSlot = uiInventorySlot[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }
}
