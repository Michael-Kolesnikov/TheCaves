using UnityEngine;
using UnityEngine.EventSystems;

public class InvSlot : MonoBehaviour, IDropHandler
{
    public Transform invItem;
    public UIInventory uiInventory;
    public InventorySlot slot;

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InvItem inventoryItem = eventData.pointerDrag.GetComponent<InvItem>();
            var uiSlotBefore = inventoryItem.parentAfterDrag.GetComponent<InvSlot>();
            inventoryItem.parentAfterDrag = transform;
            var uiSlotAfer = inventoryItem.parentAfterDrag.GetComponent<InvSlot>();
            var inventory = uiInventory.Inventory;
            inventory.TransferFromSlotToSlot(uiSlotBefore.slot, uiSlotAfer.slot);
        }
    }
}
