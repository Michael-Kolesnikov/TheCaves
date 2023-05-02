using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemScriptableObject itemScriptableObject;
    public int maxItemsInInventorySlot => itemScriptableObject.maxAmountInStack;
    public int id => itemScriptableObject.id;

    public int amount;
    public InventoryItem(ItemScriptableObject item)
    {
        itemScriptableObject = item;
        this.amount = 1;
    }

    public InventoryItem(ItemScriptableObject item, int amount)
    {
        itemScriptableObject = item;
        this.amount = amount;
    }

    public override bool Equals(object other)
    {
        if (other is not InventoryItem item) return false;
        return this.id.Equals(item.id);
    }
}
