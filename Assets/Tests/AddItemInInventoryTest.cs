using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemInInventoryTest
{
    [Test]
    public void TryAddOneItemToEmptyInv()
    {
        var inventory = new Inventory(10);
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var item = ScriptableObject.CreateInstance<ItemScriptableObject>();
        item.amount = 1;
        item.maxAmountInStack = 64;
        inventoryItem1.itemScriptableObject = item;
        inventoryItem1.amount = 1;
        var isAdd = inventory.TryToAddItem(inventoryItem1);
        Assert.IsTrue(isAdd);
    }
    [Test]
    public void TryAddMoreOneItemToEmptyInv()
    {
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var inventory = new Inventory(10);
        var item = ScriptableObject.CreateInstance<ItemScriptableObject>();
        item.amount = 1;
        item.maxAmountInStack = 4;
        inventoryItem1.itemScriptableObject = item;
        inventoryItem1.amount = 4;
        var isAdd = inventory.TryToAddItem(inventoryItem1);
        Assert.IsTrue(isAdd);
    }
    [Test]
    public void TryAddItemToStack()
    {
        var inventory = new Inventory(10);
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var item = ScriptableObject.CreateInstance<ItemScriptableObject>();
        item.amount = 1;
        item.maxAmountInStack = 4;
        inventoryItem1.itemScriptableObject = item;
        inventoryItem1.amount = 4;
        inventory.TryToAddItem(inventoryItem1);
        var isAdd = inventory.TryToAddItem(inventoryItem1);
        Assert.IsTrue(isAdd);
    }
    [Test]
    public void TryAddItemToFullInventory()
    {
        var inventory = new Inventory(2);
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var item = ScriptableObject.CreateInstance<ItemScriptableObject>();
        item.amount = 1;
        item.maxAmountInStack = 4;
        inventoryItem1.itemScriptableObject = item;
        inventoryItem1.amount = 4;
        inventory.TryToAddItem(inventoryItem1);
        inventory.TryToAddItem(inventoryItem1);

        var isAdd = inventory.TryToAddItem(inventoryItem1);
        Assert.IsFalse(isAdd);
    }
}
