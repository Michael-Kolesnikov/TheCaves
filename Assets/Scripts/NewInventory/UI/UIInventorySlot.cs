using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : MonoBehaviour , IDropHandler
{
    private InventorySlot slot;
    private UIInventoryItem _uiInventoryItem;
    private UIInventory _uiInventory;
    public void OnDrop(PointerEventData eventData)
    {
        var otherItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var otherSlotUI = otherItem.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.slot;
        var inventory = _uiInventory.inventory;
        inventory.TransferFromSlotToSlot(otherSlot, slot);
        Refresh();
        otherSlotUI.Refresh();
    }
    public void SetSlot(InventorySlot slot)
    {
        this.slot = slot;
    }
    public void SetUIItem(UIInventoryItem item)
    {
        _uiInventoryItem = item;
    }
    public void SetInventory(UIInventory uiInventory)
    {
        _uiInventory = uiInventory;
    }
    public void Refresh()
    {
        if(slot != null)
            _uiInventoryItem.Refresh(slot);
    }

    
}
