using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    public Transform craftingPanel;
    public Transform additionPanel;
    public Transform craftingArea;
    public Transform craftInfoArea;

    public List<CraftItem> craftToolsItems;
    public List<CraftItem> craftBasicsItems;
    public List<CraftItem> craftBuildItems;

    private void Start()
    {
        LoadBasicCraft();
    }

    public void OpenCraftPanel()
    {
        additionPanel.gameObject.SetActive(false);
        craftingPanel.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            additionPanel.gameObject.SetActive(true);
            craftingPanel.gameObject.SetActive(false);
        }
    }

    public void LoadBasicCraft()
    {
        DestroyCraftingAreaChildren();

        foreach (var craft in craftBasicsItems)
        {
            CreateImageObject(craft);
        }
    }

    public void LoadToolsCraft()
    {
        DestroyCraftingAreaChildren();

        foreach (var craft in craftToolsItems)
        {
            CreateImageObject(craft);
        }
    }

    public void LoadBuildCraft()
    {
        DestroyCraftingAreaChildren();

        foreach (var craft in craftBuildItems)
        {
            CreateImageObject(craft);
        }
    }

    private GameObject CreateImageObject(CraftItem craft)
    {
        var item = new GameObject();
        item.AddComponent<Image>();
        item.AddComponent<EventTrigger>();
        item.GetComponent<Image>().sprite = craft.finalCraft.spriteIcon;
        item.transform.SetParent(this.craftingArea);

        EventTrigger trigger = item.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { CreateItem(craft); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry info = new EventTrigger.Entry();
        info.eventID = EventTriggerType.PointerEnter;
        info.callback.AddListener((eventData) => { EnableInfo(craft); });
        trigger.triggers.Add(info); ;
        return item;
    }

    private void EnableInfo(CraftItem craft)
    {
        var text = "";
        text += craft.finalCraft.itemName + ":\n";
        foreach(var resource in craft.craftResources)
        {
            text += resource.item.itemName + " - " + resource.craftAmount + "\n";
        }
        craftInfoArea.GetChild(0).GetComponent<TMP_Text>().text = text;
    }
    private void CreateItem(CraftItem craft)
    {
        var inventory = this.transform.GetComponent<PickUpItems>().inventory;
        bool isResourcesEnough = true;
        foreach (var resource in craft.craftResources)
        {
            var requestAmount = resource.craftAmount;
            foreach(var slot in inventory.slots)
            {
                if(!slot.isEmpty && slot.item.id == resource.item.id)
                {
                    requestAmount -= slot.amount;
                }
            }

            if(requestAmount > 0)
            {
                isResourcesEnough = false;
                break;
            }
        }
        if (isResourcesEnough)
        {
            var added = inventory.TryToAddItem(new InventoryItem(craft.finalCraft));
            if (added)
            {
                foreach(var resource in craft.craftResources)
                {
                    inventory.TryToRemove(new InventoryItem(resource.item), resource.craftAmount);
                }
            }
            else
            {
                inventory.TryToRemove(new InventoryItem(craft.finalCraft),1);
            }
        }
        this.transform.GetComponent<PickUpItems>().uiInventory.Refresh();
        GlobalEvenManager.OnInventoryStateChange?.Invoke();
    }

    private void DestroyCraftingAreaChildren()
    {
        for (var i = 0; i < craftingArea.childCount; i++)
        {
            Destroy(craftingArea.GetChild(i).gameObject);
        }
    }
}
