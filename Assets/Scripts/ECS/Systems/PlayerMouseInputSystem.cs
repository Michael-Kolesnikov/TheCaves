using Leopotam.EcsLite;
using UnityEngine;
/// <summary>
/// system to rotate the camera with the mouse
/// </summary>
public sealed class PlayerMouseInputSystem : IEcsRunSystem
{
    private float _moveX;
    private float _moveY;

    private float _xRotation = 0f;
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<MouseLookDirectionComponent>().Inc<PlayerTag>().Inc<ModelComponent>().End();
        var mouseLookDirectionPool = system.GetWorld().GetPool<MouseLookDirectionComponent>();
        var modelPool = system.GetWorld().GetPool<ModelComponent>();

        SetDirection();

        foreach (var entity in filter)
        {
            ref var mouseLookDirection = ref mouseLookDirectionPool.Get(entity);
            if (!CharacterAbilities.canCameraMove) continue;

            mouseLookDirection.direction.x = _moveX * mouseLookDirection.mouseSensitivity * Time.deltaTime;
            mouseLookDirection.direction.y = _moveY * mouseLookDirection.mouseSensitivity * Time.deltaTime; ;

            _xRotation -= mouseLookDirection.direction.y;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            mouseLookDirection.cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            ref var character = ref modelPool.Get(entity);
            character.modelTransform.Rotate(Vector3.up * mouseLookDirection.direction.x);
        }
    }
    private void SetDirection()
    {
        _moveX = Input.GetAxisRaw("Mouse X");
        _moveY = Input.GetAxisRaw("Mouse Y");
    }
}
