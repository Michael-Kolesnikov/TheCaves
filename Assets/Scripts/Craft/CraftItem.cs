using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Craft/Item")]
public class CraftItem : ScriptableObject
{
    public ItemScriptableObject finalCraft;
    public int finalCraftItemAmount = 1;
    public List<CraftResourceItem> craftResources;
}
[System.Serializable]
public class CraftResourceItem
{
    public ItemScriptableObject item;
    public int craftAmount;
}