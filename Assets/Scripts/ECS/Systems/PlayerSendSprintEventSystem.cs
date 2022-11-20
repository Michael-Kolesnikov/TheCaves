using UnityEngine;
using Leopotam.EcsLite;
public class PlayerSendSprintEventSystem : IEcsRunSystem, IEcsInitSystem
{
    public void Init(EcsSystems system)
    {
        var playerFilter = system.GetWorld().Filter<PlayerTag>().End();
        var sprintPool = system.GetWorld().GetPool<SprintComponent>();
        foreach(var entity in playerFilter)
        {
            sprintPool.Add(entity);
            ref var sprint = ref sprintPool.Get(entity);
            sprint.accelerationFactor = 1.70f;
        }
    }
    public void Run(EcsSystems system)
    {
        var playerFilter = system.GetWorld().Filter<PlayerTag>().End();

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
            sprint.isRunning = true;
        }
    }
}
