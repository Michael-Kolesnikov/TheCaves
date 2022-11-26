using UnityEngine;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;
using Leopotam.EcsLite.ExtendedSystems;
public sealed class EcsGameStartUp : MonoBehaviour
{
    /// <summary>
    /// Contain all entities
    /// </summary>
    private EcsWorld world;
    /// <summary>
    /// Contain all sytems
    /// </summary>
    private EcsSystems _systems;

    private void Start()
    {
        world = new EcsWorld();
        _systems = new EcsSystems(world);

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
            .Add(new MovementSystem())
            .Add(new TestSystem())
            .Add(new PlayerScroolSystem())
            .Add(new PlayerInitInventorySystem())
            .Add(new PlayerOpenInventorySystem())
            .Add(new CursorLockSystem())
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
        world.Destroy();
        world = null;
    }
}
