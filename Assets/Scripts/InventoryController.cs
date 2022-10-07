using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryController : MonoBehaviour
{
    private bool isOpened = false;
    public GameObject UIPanel;
    public Transform InventoryPanel;
    public List<InventorySlot> itemList = new List<InventorySlot>();
    Camera mainCamera;
    float reachDistance = 3f;

    void Start()
    {
        mainCamera = Camera.main;
        UIPanel.SetActive(false);
        for(int i = 0; i< InventoryPanel.childCount; i++)
        {
            itemList.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
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
                    AddItemToInventory(item.itemScriptableObject, item.amount);
                    Destroy(hit.collider.gameObject);
                }
            }
            
        }

    }

    private void AddItemToInventory(ItemScriptableObject itemScriptableObject, int amount)
    {
        foreach(InventorySlot slot in itemList)
        {
            if(slot.currentItem == itemScriptableObject)
            {
                slot.amount += amount;
                slot.itemAmountText.text = slot.amount.ToString();
                return;
            }
        }
        foreach(InventorySlot slot in itemList)
        {
            if(slot.isEmpty)
            {
                slot.currentItem = itemScriptableObject;
                slot.amount = amount;
                slot.isEmpty = false;
                slot.SetIcon(itemScriptableObject.icon);
                slot.itemAmountText.text = amount.ToString();
                break;
            }
        }
    }

    private void CursorChangeState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
}
