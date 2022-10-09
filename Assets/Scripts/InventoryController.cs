using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class InventoryController : MonoBehaviour
{
    private bool isOpened = false;
    public GameObject UIPanel;
    public Transform InventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    Camera mainCamera;
    float reachDistance = 3f;
    public int capacity { get; set; }

    void Start()
    {
        mainCamera = Camera.main;
        UIPanel.SetActive(false);

        ///Add Slots to inventory
        for(int i = 0; i< InventoryPanel.childCount; i++)
        {
            slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
    }
    public void TransferFromSlotToSlot( InventorySlot slotFrom, InventorySlot slotTo)
    {
        if (slotFrom.isEmpty)
            return;
        if (slotTo.isFull)
            return;
        if (!slotTo.isEmpty && slotFrom.item.id != slotTo.item.id)
            return;

        int slotCapacity = slotFrom.capacity;
        var filled = slotFrom.amount + slotTo.amount <= slotCapacity;
        int amountToAdd = filled ? slotFrom.amount : slotCapacity - slotTo.amount;
        var amountLeft = slotFrom.amount - amountToAdd;

        if (slotTo.isEmpty)
        {
            //slotTo.SetItem(slotFrom.item);
            slotFrom.Clear();
        }
        //slotTo.item.amount += amountToAdd;
        if (filled)
            slotFrom.Clear();
        //else
            //slotFrom.item.amount = amountLeft;

    }
    void Update()
    {
        /// Open inventory when pressing "I"
        if (Input.GetKeyDown(KeyCode.I))    
        {
            isOpened = !isOpened;
            bool state = isOpened;
            UIPanel.SetActive(state);
            CursorChangeState(state);
            CharacterMoving.isMove = !state;
            CameraController.isMove = !state;
        }
        /// Pick up item 
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider?.gameObject.GetComponent<Item>() != null)
                {
                    var item = hit.collider.gameObject.GetComponent<Item>();
                    TryToAddItem(item);
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

    public bool TryToAddItem(Item item)
    {
        var slotWithSameItemButNotEmpty = slots.Find(slot => !slot.isEmpty && !slot.isFull && slot.item.id == item.id);

        if (slotWithSameItemButNotEmpty != null)
            return TryAddItemToSlot(slotWithSameItemButNotEmpty,item);
        var emptySlot = slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
            return TryAddItemToSlot(emptySlot, item);
        return false;
    }

    private bool TryAddItemToSlot(InventorySlot slot, Item item)
    {
        bool enoughSpaceForItemAmount = slot.amount + item.amount <= item.maxItemsInInventorySlot;
        int amountToAdd = enoughSpaceForItemAmount ? item.amount : item.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.amount - amountToAdd;
        if (slot.isEmpty)
            slot.SetItem(item.itemScriptableObject,amountToAdd);
        else
        {
            slot.amount += amountToAdd;
            slot.SetTextAmount(slot.amount);
        }

        if (amountLeft <= 0)
            return true;

        item.amount = amountLeft;
        return TryToAddItem(item);
    }

    public bool TryToRemove(int amount = 1)
    {
        return false;
    }

    
}
