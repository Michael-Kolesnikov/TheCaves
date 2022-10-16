using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDropItem : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{ 
    public InventorySlot slot;
    private RectTransform _RectTransform;

    private void Await()
    {
        _RectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {   
    }

    public void OnDrag(PointerEventData eventData)
    {
        _RectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var otherItem = eventData.pointerDrag.GetComponent<InventorySlot>();
        
        
    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    //{
    //    InventoryController inventoryController;
    //    InventorySlot currentSlot;
    //    Image currentSlotItem;
    //    public Image followMouseItemSprite;
    //    bool isDraging;
    //    void Start()
    //    {
    //        inventoryController = GetComponent<InventoryController>();    
    //    }

    //    void Update()
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            currentSlot = GetItemSlotUnderMouse()?.GetComponent<InventorySlot>();
    //        }
    //        if(Input.GetMouseButton(0))
    //        {
    //            if (currentSlot && !currentSlot.GetComponent<InventorySlot>().isEmpty)
    //            {
    //                followMouseItemSprite.color = new Color(255,255,255,255);
    //                //followMouseItemSprite.sprite = currentSlot.GetComponent<InventorySlot>().currentItem.icon; 
    //                followMouseItemSprite.transform.position = Input.mousePosition;
    //                isDraging = true;

    //            }
    //        }
    //        if (Input.GetMouseButtonUp(0) && isDraging)
    //        {


    //            followMouseItemSprite.color = new Color(0, 0, 0, 0);
    //            followMouseItemSprite.sprite = null;
    //            isDraging = false;
    //        }

    //    }

    //    private GameObject GetItemSlotUnderMouse()
    //    {
    //        GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
    //        PointerEventData evenData = new PointerEventData(EventSystem.current);
    //        evenData.position = Input.mousePosition;
    //        List<RaycastResult> raycastResult = new List<RaycastResult>();
    //        raycaster.Raycast(evenData, raycastResult);
    //        foreach(var obj in raycastResult)
    //        {
    //            if(obj.gameObject.GetComponent<InventorySlot>())
    //            {
    //                return obj.gameObject;
    //            }
    //        }
    //        return null;
    //    }
}
