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
    public ItemType itemType;
    public int id { get; private set; }
    public GameObject prefabItem;
    public Sprite icon;
    public int maxAmountInStack;
}
