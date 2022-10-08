using UnityEngine;
public enum ItemType
{
    Default,
    Tool,
    Weapon,
    Food,
    Construction,
}
    
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public ItemType itemType;
    public int maxAmount;
    public GameObject prefabItem;
}
