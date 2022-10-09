using System;
using UnityEngine;

public class Item : MonoBehaviour 
{
    public ItemScriptableObject itemScriptableObject;
    public int maxItemsInInventorySlot => itemScriptableObject.maxAmountInStack;
    public int amount { get; set; }
}
