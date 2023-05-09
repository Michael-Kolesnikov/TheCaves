using Leopotam.EcsLite;
using UnityEngine;

public sealed class PlayerOpenInventorySystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<PlayerInventoryComponent>().Inc<MovableComponent>().Inc<MouseLookDirectionComponent>().End();
        foreach (var entity in filter)
        {
            ref var inventory = ref system.GetWorld().GetPool<PlayerInventoryComponent>().Get(entity);
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!CharacterAbilities.canOpenInventory) continue;
                inventory.isInventoryOppened = !inventory.isInventoryOppened;
                bool state = inventory.isInventoryOppened;
                inventory.playerInventoryUIPanel.gameObject.SetActive(state);
                if (inventory.isInventoryOppened)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                ref var movable = ref system.GetWorld().GetPool<MovableComponent>().Get(entity);
                ref var mouseLook = ref system.GetWorld().GetPool<MouseLookDirectionComponent>().Get(entity);
                CharacterAbilities.canCameraMove = !CharacterAbilities.canCameraMove;
                CharacterAbilities.canCharacterMove = !CharacterAbilities.canCharacterMove;
            }
        }
    }
}
