
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IInventorySlot
{
    public bool isFull => !isEmpty && (amount == capacity);

    public bool isEmpty => item == null;

    public IInventoryItem item { get; private set; }

    public int amount => isEmpty ? 0 : item.amount;

    public int capacity { get; private set; }
    public void SetItem(IInventoryItem item)
    {
        if (!isEmpty)
            return;
        this.item = item;
        this.capacity = item.maxItemsInInventorySlot;
    }
    public void Clear()
    {
        if (!isEmpty)
            return;
        item = null;
        capacity = 0;
    }

    
}
