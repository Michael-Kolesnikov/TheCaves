using Leopotam.EcsLite;
using UnityEngine;
sealed class CursorLockSystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<PlayerInventoryComponent>().End();
        foreach(var entity in filter)
        {
            ref var inventory = ref system.GetWorld().GetPool<PlayerInventoryComponent>().Get(entity);
            if(inventory.isInventoryOppened)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
