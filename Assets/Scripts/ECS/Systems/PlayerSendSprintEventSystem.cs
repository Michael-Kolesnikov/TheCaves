using Leopotam.EcsLite;
using UnityEngine;
public sealed class PlayerSendSprintEventSystem : IEcsRunSystem, IEcsInitSystem
{
    public void Init(EcsSystems system)
    {
        var playerFilter = system.GetWorld().Filter<PlayerTag>().End();
        var sprintPool = system.GetWorld().GetPool<SprintComponent>();
        foreach (var entity in playerFilter)
        {
            sprintPool.Add(entity);
            ref var sprint = ref sprintPool.Get(entity);
            sprint.accelerationFactor = 1.70f;
            sprint.decreaseStaminaFactor = 30f;
            sprint.increaseStaminaFactor = 20f;
        }
    }
    public void Run(EcsSystems system)
    {
        var playerFilter = system.GetWorld().Filter<PlayerTag>().Inc<StaminaComponent>().End();

        var sprintEventPool = system.GetWorld().GetPool<SprintEvent>();
        var sprintPool = system.GetWorld().GetPool<SprintComponent>();

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            foreach (var entity in playerFilter)
            {
                ref var sprint = ref sprintPool.Get(entity);
                sprint.isRunning = false;
            }
            return;
        }
        foreach (var entity in playerFilter)
        {
            sprintEventPool.Add(entity);
            ref var sprint = ref sprintPool.Get(entity);
            ref var stamina = ref system.GetWorld().GetPool<StaminaComponent>().Get(entity);
            sprint.isRunning = stamina.currentStaminaValue > 0;

        }
    }
}
