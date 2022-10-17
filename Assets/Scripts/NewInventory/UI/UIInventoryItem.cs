using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIInventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textAmount;
    public ItemScriptableObject inventoryItem;
    private RectTransform _rectTransform;


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void Refresh(InventorySlot slot)
    {
        if(slot.isEmpty)
        {
            Clean();
            return;
        }
        inventoryItem = slot.item;
        _imageIcon.sprite = inventoryItem.spriteIcon;
        _textAmount.text = slot.amount == 0 ? "" : slot.amount.ToString();
    }
    public void Clean()
    {
        _imageIcon.sprite = default;
        _textAmount.text = default;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = _rectTransform.parent;
        slotTransform.SetAsFirstSibling();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
}
