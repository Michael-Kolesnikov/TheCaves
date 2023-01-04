using Leopotam.EcsLite;
using UnityEngine;

public sealed class PlayerJumpSystem : IEcsRunSystem 
{
    public void Run(EcsSystems system)
    {
        var jumpFilter = system.GetWorld().Filter<PlayerTag>().Inc<JumpEvent>().Inc<JumpComponent>().Inc<GroundCheckSphereComponent>().Inc<MovableComponent>().End();
        
        var movablePool = system.GetWorld().GetPool<MovableComponent>();
        var groundCheckPool = system.GetWorld().GetPool<GroundCheckSphereComponent>();
        var jumpPool = system.GetWorld().GetPool<JumpComponent>();

        foreach(var entity in jumpFilter)
        {
            ref var movable = ref movablePool.Get(entity);
            ref var groundCheck = ref groundCheckPool.Get(entity);
            ref var jump = ref jumpPool.Get(entity);

            ref var gravity = ref movable.gravity;
            ref var velocity = ref movable.velocity;
            ref var jumpForce = ref jump.force;

            if (!groundCheck.isGrounded) continue;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        }
    }
}
