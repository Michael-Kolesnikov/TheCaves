using UnityEngine;

public class Item : MonoBehaviour, IInventoryItem   
{
    public ItemScriptableObject itemScriptableObject;

    public bool isEquipped { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int maxItemsInInventorySlot => itemScriptableObject.maxAmount;
    public int amount { get; set; }
    public IInventoryItem Clone()
    {
        return null;
    }
}
