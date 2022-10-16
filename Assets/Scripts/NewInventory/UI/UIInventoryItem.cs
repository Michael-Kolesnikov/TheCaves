using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textAmount;

    public ItemScriptableObject inventoryItem;
    
    public void Refresh(InventorySlot slot)
    {
        if(slot.isEmpty)
        {
            Clean();
            return;
        }

        inventoryItem = slot.item;
        _imageIcon.sprite = inventoryItem.spriteIcon;
        _textAmount.text = inventoryItem.amount == 0 ? "" : inventoryItem.amount.ToString();
    }
    public void Clean()
    {
        _imageIcon = default;
        _textAmount.text = default;
    }
}
