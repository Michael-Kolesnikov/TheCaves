using Leopotam.EcsLite;
using UnityEngine;

/// <summary>
/// read player position
/// </summary>
public sealed class PlayerInputSystem : IEcsRunSystem
{
    private float _moveX;
    private float _moveZ;
    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<DirectionComponent>().End();
        SetDirection();
        var directions = systems.GetWorld().GetPool<DirectionComponent>();

        foreach (int entity in filter)
        {
            ref var directionComponent = ref directions.Get(entity);
            ref var direction = ref directionComponent.direction;
            direction.x = _moveX;
            direction.z = _moveZ;
        }
    }
    private void SetDirection()
    {
        _moveX = Input.GetAxis("Horizontal");
        _moveZ = Input.GetAxis("Vertical");
    }
}
