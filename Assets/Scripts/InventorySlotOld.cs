
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlotOld : MonoBehaviour
{
    public bool isFull => !isEmpty && (amount == capacity);
    public bool isEmpty => item == null;
    private int _amount;
    public int amount //UI
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = value;
            SetTextAmount(value);
        }
    }
    public ItemScriptableObject item;
    public int capacity { get; private set; }

    public Sprite spriteIcon; //UI
    public TMP_Text itemAmountText; //UI
    public void SetItem(ItemScriptableObject item,int amount)
    {
        if (!isEmpty)
            return;
        this.item = item;
        this.amount = amount;
        capacity = item.maxAmountInStack;
        spriteIcon = item.spriteIcon;
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
    public void SetTextAmount(int amount) //UI
    {
        itemAmountText.text = amount.ToString();

    }
}
