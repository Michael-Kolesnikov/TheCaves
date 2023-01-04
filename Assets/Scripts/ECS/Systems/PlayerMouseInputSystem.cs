using Leopotam.EcsLite;
using UnityEngine;
/// <summary>
/// system to rotate the camera with the mouse
/// </summary>
public sealed class PlayerMouseInputSystem : IEcsRunSystem
{
    private float moveX;
    private float moveY;

    private float xRotation = 0f;
    public void Run(EcsSystems system)
    {
        var filter =  system.GetWorld().Filter<MouseLookDirectionComponent>().Inc<PlayerTag>().Inc<ModelComponent>().End();
        var mouseLookDirectionPool = system.GetWorld().GetPool<MouseLookDirectionComponent>();
        var modelPool = system.GetWorld().GetPool<ModelComponent>();

        SetDirection();

        foreach(var entity in filter)
        {
            ref var mouseLookDirection = ref mouseLookDirectionPool.Get(entity);
            if (!mouseLookDirection.canMove) continue;

            mouseLookDirection.direction.x = moveX * mouseLookDirection.mouseSensitivity * Time.deltaTime;
            mouseLookDirection.direction.y = moveY * mouseLookDirection.mouseSensitivity * Time.deltaTime; ;

            xRotation -= mouseLookDirection.direction.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            mouseLookDirection.cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            ref var character = ref modelPool.Get(entity);
            character.modelTransform.Rotate(Vector3.up * mouseLookDirection.direction.x);
        }
    }
    private void SetDirection()
    {
        moveX = Input.GetAxisRaw("Mouse X");
        moveY = Input.GetAxisRaw("Mouse Y");
    }
}
