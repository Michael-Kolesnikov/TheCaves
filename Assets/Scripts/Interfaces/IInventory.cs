public interface IInventory
{
    int capacity { get; set; }
    bool isFull { get; }
    bool TryToAdd(IInventoryItem item); 
    bool TryToRemove(int amount = 1);

}
