using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class InventoryController : MonoBehaviour, IInventory
{
    private bool isOpened = false;
    public GameObject UIPanel;
    public Transform InventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    Camera mainCamera;
    float reachDistance = 3f;
    public int capacity { get; set; }

    public bool isFull => slots.All(slot => slot.isFull);

    void Start()
    {
        mainCamera = Camera.main;
        UIPanel.SetActive(false);
        for(int i = 0; i< InventoryPanel.childCount; i++)
        {
            slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
    }
    public void TransferFromSlotToSlot( IInventorySlot slotFrom,IInventorySlot slotTo)
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))    
        {
            isOpened = !isOpened;
            bool state = isOpened;
            UIPanel.SetActive(state);
            CursorChangeState(state);
            CharacterMoving.isMove = !state;
            CameraController.isMove = !state;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider?.gameObject.GetComponent<Item>() != null)
                {
                    var item = hit.collider.gameObject.GetComponent<Item>();
                    TryToAdd(item);
                    Destroy(hit.collider.gameObject);
                }
            }
            
        }

    }
    private void CursorChangeState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }

    public bool TryToAdd(IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = slots.Find(slot => !slot.isEmpty && !slot.isFull);
        if (slotWithSameItemButNotEmpty != null)
            return TryAddToSlot(slotWithSameItemButNotEmpty,item);
        var emptySlot = slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
            return TryAddToSlot(emptySlot, item);
        return false;
    }

    private bool TryAddToSlot(InventorySlot slot, IInventoryItem item)
    {
        var filled = slot.amount + item.amount <= item.maxItemsInInventorySlot;
        var amountToAdd = filled ? item.amount : item.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.amount - amountToAdd;
        var clonedItem = item; /// ????
        clonedItem.amount = amountToAdd;

        if (slot.isEmpty)
            slot.SetItem(clonedItem);
        else
            slot.item.amount += amountToAdd;
        if (amountLeft <= 0)
            return true;
        item.amount = amountLeft;
        return TryToAdd(item);
    }

    public bool TryToRemove(int amount = 1)
    {
        return false;
    }

    
}
