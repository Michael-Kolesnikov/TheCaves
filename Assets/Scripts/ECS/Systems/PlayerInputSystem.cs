using Leopotam.EcsLite;
using UnityEngine;
public sealed class PlayerInputSystem : IEcsRunSystem
{
    private EcsFilter _directionFilter = null;
    private EcsWorld _world = null;

    private float moveX;
    private float moveZ;
    public void Run(EcsSystems systems)
    {
        _world = systems.GetWorld();
        _directionFilter = _world.Filter<DirectionComponent>().End();

        SetDirection();
        var directions = _world.GetPool<DirectionComponent>();

        foreach (int entity in _directionFilter)
        {
            ref var directionComponent = ref directions.Get(entity);
            ref var direction = ref directionComponent.direction;
            direction.x = moveX;
            direction.z = moveZ;
        }
    }
    private void SetDirection()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }
}
