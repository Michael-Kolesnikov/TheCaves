using Leopotam.EcsLite;
public class PlayerInitInventorySystem : IEcsInitSystem
{
    public void Init(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<PlayerInventoryComponent>().End();
        var inventroyPool = system.GetWorld().GetPool<PlayerInventoryComponent>();

        foreach (var entity in filter)
        {
            ref var inventoryComponent = ref inventroyPool.Get(entity);
            inventoryComponent.isInventoryOppened = false;
            inventoryComponent.inventory = new Inventory(inventoryComponent.playerInventoryUIPanel.GetChild(0).childCount);
            inventoryComponent.UIInventory = new UIInventory(inventoryComponent.inventory,inventoryComponent.playerInventoryUIPanel.GetChild(0));
            inventoryComponent.inventory.UIInventory = inventoryComponent.UIInventory;
        }
    }
}
