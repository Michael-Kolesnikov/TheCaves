
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isEmpty = true;
    public ItemScriptableObject currentItem;
    public int amount;
    public Sprite icon;
    public Sprite defaultIcon;
    public TMP_Text itemAmountText;
    private void Start()
    {
        GetComponent<Image>().sprite = defaultIcon;
    }
    public void SetIcon(Sprite spriteIcon)
    {
        icon = spriteIcon;
        GetComponent<Image>().sprite = icon;
    }
    public void ClearSlot()
    {
        isEmpty = true;
        SetIcon(defaultIcon);
        currentItem = null;
        itemAmountText.text = string.Empty;
    }
    public void Swap(InventorySlot slot1, InventorySlot slot2)
    {
        var tempSlot = new InventorySlot();
        tempSlot.icon = slot1.icon;
        tempSlot.amount = slot1.amount;
        tempSlot.currentItem = slot1.currentItem;
        tempSlot.isEmpty = slot1.isEmpty;
        tempSlot.itemAmountText = slot1.itemAmountText;

        slot1.SetIcon(slot2.icon);
        slot1.amount = slot2.amount;
        slot1.currentItem = slot2.currentItem;
        slot1.itemAmountText = slot2.itemAmountText;
        slot1.isEmpty = slot2.isEmpty;

        slot2.SetIcon(tempSlot.icon);
        slot2.amount = tempSlot.amount;
        slot2.itemAmountText = tempSlot.itemAmountText;
        slot2.currentItem = tempSlot.currentItem;
        slot2.isEmpty = tempSlot.isEmpty;
    }
    public void AddDataIntoSlot(InventorySlot newSlot)
    {
        this.amount = newSlot.amount;
        this.currentItem = newSlot?.currentItem;
        this.SetIcon(newSlot ?.icon);
    }
}
