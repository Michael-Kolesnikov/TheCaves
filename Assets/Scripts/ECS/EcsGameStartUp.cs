using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Voody.UniLeo.Lite;

public sealed class EcsGameStartUp : MonoBehaviour
{
    /// Contain all entities
    private EcsWorld _world;
    /// Contain all sytems
    private EcsSystems _systems;

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _systems.ConvertScene();
        AddSystems();
    }
    private void Update()
    {
        _systems.Run();
    }
    private void AddSystems()
    {
        _systems
            .Add(new PlayerSendJumpEventSystem())
            .Add(new PlayerSendSprintEventSystem())
            .Add(new GroundCheckSystem())
            .Add(new PlayerInputSystem())
            .Add(new PlayerMouseInputSystem())
            .Add(new GravitySystem())
            .Add(new PlayerJumpSystem())
            .Add(new PlayerMovementSystem())
            .Add(new PlayerScroolSystem())
            .Add(new PlayerInitInventorySystem())
            .Add(new PlayerOpenInventorySystem())
            .Add(new CursorLockSystem())
            .Add(new FatigueSystem())
            .Add(new PlayerRestSystem())
            .Add(new PlayerUpdateHUDValuesSystem())
            .Add(new ChunksGenerationSystem())
            .Add(new LKM())
            //.Add(new PKM())
            .Add(new PlayerPickupItem())
            .DelHere<JumpEvent>()
            .DelHere<SprintEvent>()
            .Init();
        Debug.Log("systems added");
    }   
    private void OnDestroy()
    {
        if (_systems == null) return;
        _systems.Destroy();
        _systems = null;
        _world.Destroy();
        _world = null;
    }
}
