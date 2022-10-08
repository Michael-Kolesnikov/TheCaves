using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDropItem : MonoBehaviour
{
    InventoryController inventoryController;
    InventorySlot currentSlot;
    Image currentSlotItem;
    public Image followMouseItemSprite;
    void Start()
    {
        inventoryController = GetComponent<InventoryController>();    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentSlot = GetItemSlotUnderMouse()?.GetComponent<InventorySlot>();
        }
        if(Input.GetMouseButton(0))
        {
            if (currentSlot && !currentSlot.GetComponent<InventorySlot>().isEmpty)
            {
                followMouseItemSprite.color = new Color(255,255,255,255);
                followMouseItemSprite.sprite = currentSlot.GetComponent<InventorySlot>().currentItem.icon; 
                followMouseItemSprite.transform.position = Input.mousePosition;

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            var newSlot = GetItemSlotUnderMouse()?.GetComponent<InventorySlot>();
            if(newSlot)
            {
                if(newSlot.isEmpty)
                {
                    newSlot.isEmpty = false;
                    newSlot.SetIcon(followMouseItemSprite.sprite);
                    newSlot.itemAmountText.text = currentSlot.itemAmountText.text;
                    newSlot.currentItem = currentSlot.currentItem;
                    currentSlot.ClearSlot();
                }
                else
                {
                    newSlot.Swap(newSlot, currentSlot);
                }
                
            }
                followMouseItemSprite.color = new Color(0, 0, 0, 0);
                followMouseItemSprite.sprite = null;
        }
    }

    private GameObject GetItemSlotUnderMouse()
    {
        GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
        PointerEventData evenData = new PointerEventData(EventSystem.current);
        evenData.position = Input.mousePosition;
        List<RaycastResult> raycastResult = new List<RaycastResult>();
        raycaster.Raycast(evenData, raycastResult);
        foreach(var obj in raycastResult)
        {
            if(obj.gameObject.GetComponent<InventorySlot>())
            {
                return obj.gameObject;
            }
        }
        return null;
    }
}
