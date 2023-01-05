using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObject itemScriptableObject;
    public int maxItemsInInventorySlot => itemScriptableObject.maxAmountInStack;
    public int amount;
    public int id => itemScriptableObject.id;
    public Item Clone()
    {
        var clone = gameObject.AddComponent<Item>();
        clone.itemScriptableObject = itemScriptableObject;
        clone.amount = amount;
        return clone;
    }
}
