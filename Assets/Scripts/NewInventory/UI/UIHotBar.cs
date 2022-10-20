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
    int IndexCurrentSlot
    {
        get
        {
            return _indexCurrentSlot;
        }
        set
        {
            if(value < slots.Count && value >= 0)
                _indexCurrentSlot = value;
            else
            {
                if (value >= slots.Count)
                    _indexCurrentSlot = 0;
                else
                    _indexCurrentSlot = slots.Count - 1;
            }
            
                
        }
    }
    int _indexCurrentSlot;
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
            IndexCurrentSlot++;
            _currentScroolSlot = slots[IndexCurrentSlot];
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _currentScroolSlot.UnSelect();
            IndexCurrentSlot--;
            _currentScroolSlot = slots[IndexCurrentSlot];
        }
        _currentScroolSlot.SelectSlot();
    }       
    private void RefreshHotBarSlots()
    {
        foreach(var slot in slots)
            slot.Refresh();
    }
}
