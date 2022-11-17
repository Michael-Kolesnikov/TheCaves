using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private bool isOpened = false;
    public GameObject UIInventoryPanel;
    public Transform InventoryPanel;
    public Transform HotBarPanel;
    public Transform canvas;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public List<InventorySlot> hotbarSlots = new List<InventorySlot>();
    Camera mainCamera; //UI
    float reachDistance = 3f; //UI
    public int capacity { get; set; }
    void Start() //UI
    {
        
        mainCamera = Camera.main;
        UIInventoryPanel.SetActive(false);

        ///Add Slots to inventory
        for(int i = 0; i< InventoryPanel.childCount; i++)
        {
            slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        for (int i = 0; i < HotBarPanel.childCount; i++)
        {
            hotbarSlots.Add(HotBarPanel.GetChild(i).GetComponent<InventorySlot>());
        }
    }
    public void TransferFromSlotToSlot(InventorySlotOld slotFrom, InventorySlotOld slotTo)
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
            slotTo.SetItem(slotFrom.item, slotFrom.amount);
            slotFrom.Clear();
        }
        slotTo.amount += amountToAdd;
        if (filled)
            slotFrom.Clear();
        else
            slotFrom.amount = amountLeft;
    }
    void Update() //UI
    {
        /// Open inventory when pressing "I"
        if (Input.GetKeyDown(KeyCode.I))    
        {
            isOpened = !isOpened;
            bool state = isOpened;
            SetActiveHudElements(state);
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
                    //TryToAddItem(item);
                    RefreshHotBatUI();
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void RefreshHotBatUI() //UI
    {
        for (int i = 0; i < HotBarPanel.childCount; i++)
        {
            //hotbarSlots[i].spriteIcon = slots[i]?.spriteIcon;
            //hotbarSlots[i].GetComponent<Image>().sprite = slots[i]?.spriteIcon;
            //if(slots[i].amount != 0)
            //    hotbarSlots[i].SetTextAmount(slots[i].amount);
        }
    }

    private void SetActiveHudElements(bool state) //UI
    {
        for (var i = 0; i < canvas.childCount; i++)
        {
            if (canvas.GetChild(i).gameObject != UIInventoryPanel.gameObject)
            {
                if (canvas.GetChild(i).GetComponent<Image>() != null)
                    canvas.GetChild(i).GetComponent<Image>()?.gameObject.SetActive(!state);
            }
            else
                canvas.GetChild(i).GetComponent<Image>()?.gameObject.SetActive(state);
        }
        CursorChangeState(state);
    }
    private void CursorChangeState(bool state) //UI
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
    
    
}
