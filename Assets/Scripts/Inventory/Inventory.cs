using System;
using System.Collections.Generic;

public class Inventory
{
    public event Action OnInventoryStateChangeEvent;
    public UIInventory UIInventory;
    public List<InventorySlot> slots;
    public int capacity { get; set; }
    public Inventory(int capacity)
    {
        this.capacity = capacity;
        slots = new List<InventorySlot>();
        for (var i = 0; i < capacity; i++)
            slots.Add(new InventorySlot());
    }
    public void TransferFromSlotToSlot(InventorySlot slotFrom, InventorySlot slotTo)
    {
        if (slotFrom.isEmpty)
            return;
        if (slotFrom.item.id.Equals(slotTo.item?.id) || slotTo.isEmpty)
        {
            if (slotTo.isFull)
                return;
            int slotCapacity = slotFrom.capacity;
            var filled = slotFrom.amount + slotTo.amount <= slotCapacity;
            int amountToAdd = filled ? slotFrom.amount : slotCapacity - slotTo.amount;
            var amountLeft = slotFrom.amount - amountToAdd;

            if (slotTo.isEmpty)
            {
                slotTo.SetItem(slotFrom.item, slotFrom.amount);
                slotFrom.Clear();
            }
            else
                slotTo.amount += amountToAdd;
            if (filled)
                slotFrom.Clear();
            else
                slotFrom.amount = amountLeft;
        }
        else
        {
            var cloneSlot = slotTo.Clone();
            slotTo.ReplaceItem(slotFrom.item, slotFrom.amount);
            slotFrom.ReplaceItem(cloneSlot.item, cloneSlot.amount);

        }
        OnInventoryStateChangeEvent?.Invoke();

    }
    public bool TryToAddItem(InventoryItem item)
    {
        var slotWithSameItemButNotEmpty = slots.Find(slot => !slot.isEmpty && !slot.isFull && slot.item.id == item.id);

        if (slotWithSameItemButNotEmpty != null)
            return TryAddItemToSlot(slotWithSameItemButNotEmpty, item);
        var emptySlot = slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
            return TryAddItemToSlot(emptySlot, item);
        return false;
    }
    private bool TryAddItemToSlot(InventorySlot slot, InventoryItem item)
    {
        bool enoughSpaceForItemAmount = slot.amount + item.amount <= item.maxItemsInInventorySlot;
        int amountToAdd = enoughSpaceForItemAmount ? item.amount : item.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.amount - amountToAdd;
        if (slot.isEmpty)
            slot.SetItem(item.itemScriptableObject, amountToAdd);
        else
            slot.amount += amountToAdd;
        OnInventoryStateChangeEvent?.Invoke();
        if (amountLeft <= 0)    
            return true;

        item.amount = amountLeft;
        return TryToAddItem(item);
    }
    public bool TryToRemove(int amount = 1)
    {
        return false;
    }
}
