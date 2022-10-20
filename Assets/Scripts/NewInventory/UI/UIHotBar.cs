using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotBar : MonoBehaviour
{
    public Canvas canvas;
    Inventory _inventory;
    private UIHotBarSlot _currentScroolSlot;
    List<UIHotBarSlot> slots;
    public Transform hotBarPanel;
    int index;
    public void SetupUIHotBarSlots()
    {
        var x = canvas.GetComponent<UIInventory>();
        _inventory = canvas.GetComponent<UIInventory>().inventory;
        _inventory.OnInventoryStateChangeEvent += RefreshHotBarSlots;
        slots = new List<UIHotBarSlot>();

        for (int i = 0; i < hotBarPanel.childCount; i++)
        {
            slots.Add(hotBarPanel.GetChild(i).GetComponent<UIHotBarSlot>());
            slots[i].SetInventorySlot(_inventory.slots[i]);
        }
        _currentScroolSlot = slots[0];
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _currentScroolSlot.UnSelect();
            index = Mathf.Clamp(index+1,0,slots.Count - 1);
            _currentScroolSlot = slots[index];
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _currentScroolSlot.UnSelect();
            index = Mathf.Clamp(index - 1, 0, slots.Count - 1);
            _currentScroolSlot = slots[index];
        }
        _currentScroolSlot.SelectSlot();
    }       
    private void RefreshHotBarSlots()
    {
        foreach(var slot in slots)
            slot.Refresh();
    }
}
