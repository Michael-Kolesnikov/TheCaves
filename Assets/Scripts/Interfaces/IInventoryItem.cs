public interface IInventoryItem
{
    bool isEquipped { get; set; }
    int maxItemsInInventorySlot { get; }
    int amount { get; set; }
    IInventoryItem Clone();

}