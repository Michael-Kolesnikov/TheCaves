using Leopotam.EcsLite;
using UnityEngine;

/// <summary>
/// A system that provides a change in the player's stamina
/// </summary>
public sealed class FatigueSystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<StaminaComponent>().Inc<SprintComponent>().End();
        foreach (var entity in filter)
        {
            ref var sprint = ref system.GetWorld().GetPool<SprintComponent>().Get(entity);
            ref var stamina = ref system.GetWorld().GetPool<StaminaComponent>().Get(entity);
            if (sprint.isRunning)
            {
                stamina.currentStaminaValue = Mathf.Max(0,stamina.currentStaminaValue - sprint.decreaseStaminaFactor * Time.deltaTime);
            }
        }   
    }
}
