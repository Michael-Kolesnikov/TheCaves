using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : MonoBehaviour , IDropHandler
{
    InventorySlot slot;
    private UIInventoryItem _uiInventoryItem;
    UIInventory uiInventory;
    
    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    public void Refresh()
    {
        if(_uiInventoryItem != null)
            _uiInventoryItem.Refresh();
    }
}
