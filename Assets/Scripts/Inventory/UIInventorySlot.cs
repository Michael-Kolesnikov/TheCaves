using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : MonoBehaviour
{
    private InventorySlot slot;
    private UIItem _uiInventoryItem;
    private UIInventory _uiInventory;
    public void OnDrop(PointerEventData eventData)
    {
        var otherItem = eventData.pointerDrag.GetComponent<UIItem>();
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

    public void SetUIItem(UIItem item)
    {
        _uiInventoryItem = item;
    }
    public void SetInventory(UIInventory uiInventory)
    {
        _uiInventory = uiInventory;
    }
    public void Refresh()
    {
        if (_uiInventoryItem != null)
            if (slot != null)
                _uiInventoryItem.Refresh(slot);
    }
}
