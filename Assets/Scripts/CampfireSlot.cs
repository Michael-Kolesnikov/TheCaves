using UnityEngine;
using UnityEngine.EventSystems;

public class CampfireSlot : MonoBehaviour, IDropHandler
{
    public Transform invItem;
    public UIInventory uiInventory;
    public InventorySlot slot;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InvItem inventoryItem = eventData.pointerDrag.GetComponent<InvItem>();
            var uiSlotBefore = inventoryItem.parentAfterDrag.GetComponent<InvSlot>();
            inventoryItem.parentAfterDrag = transform;
            var uiSlotAfer = inventoryItem.parentAfterDrag.GetComponent<CampfireSlot>();
            var inventory = uiSlotBefore.uiInventory.Inventory;
            inventory.TryToRemove(new InventoryItem(uiSlotBefore.slot.item), uiSlotBefore.slot.amount);
            //inventory.TransferFromSlotToSlot(uiSlotBefore.slot, uiSlotAfer.slot);
        }
    }
}
