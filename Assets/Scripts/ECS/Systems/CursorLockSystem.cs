using Leopotam.EcsLite;
using UnityEngine;
sealed class CursorLockSystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
