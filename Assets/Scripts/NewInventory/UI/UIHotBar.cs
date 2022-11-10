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
    public Transform player;

    public Transform hand;
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
            Scrool(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            Scrool(-1);
        _currentScroolSlot.SelectSlot();
    }       
    private void Scrool(int value)
    {
        for(var i = 0; i < hand.childCount;i++)
        {
            Destroy(hand.GetChild(i).gameObject);
        }
        _currentScroolSlot.UnSelect();
        if (value > 0)
            IndexCurrentSlot++;
        else
            IndexCurrentSlot--;
        _currentScroolSlot = slots[IndexCurrentSlot];

        if(_currentScroolSlot?.inventorySlot?.item?.prefab != null)
        {
            var item = _currentScroolSlot?.inventorySlot?.item?.prefab;
            var currentItem = Instantiate(item);
            currentItem.transform.parent = hand.transform;
            currentItem.transform.localEulerAngles = new Vector3(0, 180, 90);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void RefreshHotBarSlots()
    {
        foreach(var slot in slots)
            slot.Refresh();
    }
}
