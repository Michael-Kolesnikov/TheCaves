using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot 
{
    public bool isFull => !isEmpty && (amount == capacity);
    public bool isEmpty => item == null;
    public int amount;
    public int id => item.id;
    public ItemScriptableObject item;
    public int capacity { get; private set; }

    public void SetItem(ItemScriptableObject item, int amount)
    {
        if (!isEmpty)
            return;
        this.item = item;
        this.amount = amount;
        capacity = item.maxAmountInStack;
    }
    public void Clear()
    {
        if (!isEmpty)
            return;
        item = null;
        capacity = 0;
    }
}
