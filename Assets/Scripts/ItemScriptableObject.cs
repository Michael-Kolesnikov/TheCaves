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
    public GameObject item;
    public string itemname;
    public string description;
    public Sprite icon;
    public int maxAmount;
    private int _ID;
}
