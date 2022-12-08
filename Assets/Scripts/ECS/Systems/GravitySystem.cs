using Leopotam.EcsLite;
using UnityEngine;

public sealed class GravitySystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<MovableComponent>().End();
        var movablePool = system.GetWorld().GetPool<MovableComponent>();

        foreach(var entity in filter)
        {
            ref var movable = ref movablePool.Get(entity);

            ref var gravity = ref movable.gravity;
            ref var velocity = ref movable.velocity;

            velocity.y += gravity * Time.deltaTime;
        }
    }
}
