using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TransferItemsInInventoryTest
{
    [Test]
    public void TransferDiffItems()
    {
        var inventory = new Inventory(4);
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var item1 = new ItemScriptableObject();
        inventoryItem1.itemScriptableObject = item1;
        inventoryItem1.amount = 1;
        var myClassType = item1.GetType();
        FieldInfo myPrivateValueField = myClassType.GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);
        myPrivateValueField.SetValue(item1, 22);

        var inventoryItem2 = new GameObject().AddComponent<InventoryItem>();
        var item2 = new ItemScriptableObject();
        inventoryItem1.itemScriptableObject = item1;
        var myClassType2 = item1.GetType();
        inventoryItem2.amount = 1;
        FieldInfo myPrivateValueField2 = myClassType.GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);
        myPrivateValueField.SetValue(item1, 22);

        inventory.TryToAddItem(inventoryItem1);
        inventory.TryToAddItem(inventoryItem2);
        inventory.TransferFromSlotToSlot(inventory.slots[0], inventory.slots[1]);
        Assert.That(inventoryItem1.itemScriptableObject.id, Is.EqualTo(inventory.slots[1].item.id));
    }
    [Test]
    public void SetPrivateId()
    {
        var inventoryItem1 = new GameObject().AddComponent<InventoryItem>();
        var item1 = new ItemScriptableObject();
        inventoryItem1.itemScriptableObject = item1;
        var myClassType = item1.GetType();
        FieldInfo myPrivateValueField = myClassType.GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);
        myPrivateValueField.SetValue(item1,22);
        Assert.That(item1.id, Is.EqualTo(22));

    }
}
