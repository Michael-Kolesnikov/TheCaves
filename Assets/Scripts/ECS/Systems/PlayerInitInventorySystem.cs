
/* Необъединенное слияние из проекта "Scripts.Player"
До:
using UnityEngine;
using Leopotam.EcsLite;
После:
using Leopotam.EcsLite;
using UnityEngine;
*/
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
        }
    }
}
