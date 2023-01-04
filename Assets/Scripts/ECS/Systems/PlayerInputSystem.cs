using Leopotam.EcsLite;
using UnityEngine;
/// <summary>
/// read player position
/// </summary>
public sealed class PlayerInputSystem : IEcsRunSystem
{
    private float moveX;
    private float moveZ;
    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<DirectionComponent>().End();

        SetDirection();
        var directions = systems.GetWorld().GetPool<DirectionComponent>();

        foreach (int entity in filter)
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
