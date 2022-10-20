using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHotBarSlot : MonoBehaviour
{
    public int index { get; private set; }
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textAmount;
    [SerializeField] private Transform _activeBorder;
    InventorySlot _inventroySlot;
    public void SetInventorySlot(InventorySlot inventorySlot)
    {
        _inventroySlot = inventorySlot;
    }
    public void Refresh()
    {
        _imageIcon.sprite = _inventroySlot?.item?.spriteIcon;
        _textAmount.text = _inventroySlot.amount == 0 ? "" : _inventroySlot.amount.ToString();
    }
    public void SelectSlot()
    {
        _activeBorder.gameObject.SetActive(true);
    }
    public void UnSelect()
    {
        _activeBorder.gameObject.SetActive(false);
    }
}
