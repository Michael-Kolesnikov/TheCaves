using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIInventory : MonoBehaviour
{
    public Inventory Inventory { get; private set; }
    public Transform InventoryPanel { get; private set; }
    public List<InvSlot> inventorySlotsUI = new();
    private Transform invItemPrefab;
    public UIInventory(Inventory inventory, Transform UIInventoryPanel, Transform invItem)
    {
        this.Inventory = inventory;
        this.InventoryPanel = UIInventoryPanel;
        this.invItemPrefab = invItem;

        for (var i = 0; i < inventory.capacity; i++)
        {

            InventoryPanel.GetChild(i).GetComponent<InvSlot>().uiInventory = this;
            InventoryPanel.GetChild(i).GetComponent<InvSlot>().slot = inventory.slots[i];
            inventorySlotsUI.Add(InventoryPanel.GetChild(i).GetComponent<InvSlot>());
            
        }

    }

    public void Refresh()
    {
        for(var i = 0; i < Inventory.capacity; i++)
        {
            if (Inventory.slots[i].isEmpty)
            {
                foreach(Transform child in inventorySlotsUI[i].transform)
                {
                    Destroy(child.gameObject);
                }
                continue;
            }

            if (inventorySlotsUI[i].transform.childCount == 0)
            {
                var invItem = Instantiate(invItemPrefab);
                if (Inventory.slots[i].amount != 1)
                {
                    invItem.GetChild(0).GetComponent<TMP_Text>().text = Inventory.slots[i].amount.ToString();
                }
                else
                {
                    invItem.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
                }
                invItem.SetParent(inventorySlotsUI[i].transform);
                invItem.GetComponent<Image>().sprite = Inventory.slots[i].item.spriteIcon;
            }
            else
            {
                if (Inventory.slots[i].amount != 1)
                {
                    inventorySlotsUI[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = Inventory.slots[i].amount.ToString();
                }
                else
                {
                    inventorySlotsUI[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = string.Empty;

                }
            }
        }
    }
}
