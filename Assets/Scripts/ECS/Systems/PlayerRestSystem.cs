using Leopotam.EcsLite;
using UnityEngine;

public sealed class PlayerRestSystem : IEcsRunSystem
{
   public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<StaminaComponent>().End();
        foreach(var entity in filter)
        {
            ref var stamina = ref system.GetWorld().GetPool<StaminaComponent>().Get(entity);
            ref var sprint = ref system.GetWorld().GetPool<SprintComponent>().Get(entity);
            if(!sprint.isRunning)
                stamina.currentStaminaValue = Mathf.Min(stamina.maxStaminaValue,stamina.currentStaminaValue + sprint.increaseStaminaFactor * Time.deltaTime);
        }
    }
}
