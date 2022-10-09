
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public bool isFull => !isEmpty && (amount == capacity);
    public bool isEmpty => item == null;
    public int amount;
    public ItemScriptableObject item;
    public int capacity { get; private set; }

    public Sprite spriteIcon;
    public TMP_Text itemAmountText;
    public void SetItem(ItemScriptableObject item)
    {
        if (!isEmpty)
            return;
        this.item = item;
        capacity = item.maxAmountInStack;
        spriteIcon = item.spriteIcon;
        amount = item.amount;
        GetComponent<Image>().sprite = spriteIcon;
        SetTextAmount(amount);
    }
    public void Clear()
    {
        if (!isEmpty)
            return;
        item = null;
        capacity = 0;
    }
    public void SetTextAmount(int amount)
    {
        itemAmountText.text = amount.ToString();

    }
}
