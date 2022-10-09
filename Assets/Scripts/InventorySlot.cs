
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isFull => !isEmpty && (amount == capacity);

    public bool isEmpty => item == null;


    public int amount => isEmpty ? 0 : item.amount;
    public Item item;
    public int capacity { get; private set; }
    public void SetItem(Item item)
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
