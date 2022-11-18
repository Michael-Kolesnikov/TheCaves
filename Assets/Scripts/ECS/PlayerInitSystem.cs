using Leopotam.EcsLite;
public sealed class PlayerInitSystem : IEcsInitSystem
{
    public void Init(EcsSystems system)
    {
        EcsWorld world = system.GetWorld();

        var playerEntity = world.NewEntity();

        world.GetPool<MovableComponent>().Add(playerEntity);
        world.GetPool<DirectionComponent>().Add(playerEntity);
        world.GetPool<ModelComponent>().Add(playerEntity);
    }
}
