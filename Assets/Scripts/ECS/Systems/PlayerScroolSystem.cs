using Leopotam.EcsLite;
using UnityEngine;
public sealed class PlayerScroolSystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var playerTagPool = system.GetWorld().Filter<PlayerTag>().Inc<PlayerHotBarComponent>().End();
        var playerHotBarPool = system.GetWorld().GetPool<PlayerHotBarComponent>();
        foreach (var entity in playerTagPool)
        {
            ref var hotBar = ref playerHotBarPool.Get(entity);
            ref var hotBarCanvas = ref hotBar.hudBarCanvas;
            ref var activeSlotIndex = ref hotBar.activeSlotIndex;

            var hotbarSlotsCount = hotBarCanvas.childCount;

            //Mouse ScroolWheel UP
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                activeSlotIndex++;
            //Mouse ScroolWheel Down
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                activeSlotIndex--;

            //constantly scroll through
            if (activeSlotIndex >= hotbarSlotsCount)
                activeSlotIndex = 0;
            else if (activeSlotIndex < 0)
                activeSlotIndex = hotbarSlotsCount - 1;

            //hotbarslot.GetChild(0) must be border, which show you what slot are active
            for (var i = 0; i < hotbarSlotsCount; i++)
                hotBarCanvas.GetChild(i).GetChild(0).gameObject.SetActive(false);

            //Set active choosen slot border 
            hotBarCanvas.GetChild(activeSlotIndex).GetChild(0).gameObject.SetActive(true);
        }
    }
}
