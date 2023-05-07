using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Item/Weapon")]
public class WeaponItem : ItemScriptableObject
{
    public int damage;
    public int durability;
}
