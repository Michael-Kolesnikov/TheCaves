using Leopotam.EcsLite;
using UnityEngine;

/// <summary>
/// Surface check system
/// </summary>
public sealed class GroundCheckSystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().End();
        var groundCheckSpherePool = system.GetWorld().GetPool<GroundCheckSphereComponent>();
        foreach (var entity in filter)
        {
            ref var groundCheck = ref groundCheckSpherePool.Get(entity);
            groundCheck.isGrounded = Physics.CheckSphere(groundCheck.groundCheckSphere.position, groundCheck.groundCheckRadius, groundCheck.groundMask);
        }
    }
}
