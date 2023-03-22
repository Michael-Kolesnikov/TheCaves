using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] public Image _imageIcon;

    public ItemScriptableObject inventoryItem;
    private RectTransform _rectTransform;
    public Text amoutText;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void Refresh(InventorySlot slot)
    {
        if (slot.isEmpty)
        {
            Clean();
            return;
        }

        inventoryItem = slot.item;
        //amoutText.text = slot.item.amount.ToString();
        _imageIcon.sprite = inventoryItem.spriteIcon;
    }
    public void Clean()
    {
        _imageIcon = default;
        if(_imageIcon != null)
            _imageIcon.sprite = default;
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
