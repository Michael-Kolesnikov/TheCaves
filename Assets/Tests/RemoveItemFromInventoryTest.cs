using NUnit.Framework;
using UnityEngine;

public class RemoveItemInInventoryTest : MonoBehaviour
{
    [Test]
    public void TryRemoveOneItemFromInv()
    {
        var inventory = new Inventory(10);
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var item = ScriptableObject.CreateInstance<ItemScriptableObject>();
        item.amount = 1;
        item.maxAmountInStack = 64;
        inventoryItem1.itemScriptableObject = item;
        inventoryItem1.amount = 1;
        inventory.TryToAddItem(inventoryItem1);
        var isRemove = inventory.TryToRemove(inventoryItem1,1);

        Assert.IsTrue(isRemove && inventory.slots[0].isEmpty);
    }

    [Test]
    public void TryRemoveFromEmptyInv()
    {
        var inventory = new Inventory(10);
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var item = ScriptableObject.CreateInstance<ItemScriptableObject>();
        item.amount = 1;
        item.maxAmountInStack = 64;
        inventoryItem1.itemScriptableObject = item;
        inventoryItem1.amount = 1;
        var isRemove = inventory.TryToRemove(inventoryItem1, 1);

        Assert.IsFalse(isRemove);
    }
}
