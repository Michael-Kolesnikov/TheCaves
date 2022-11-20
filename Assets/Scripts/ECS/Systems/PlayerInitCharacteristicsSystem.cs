using Leopotam.EcsLite;
public sealed class PlayerInitCharacteristicsSystem : IEcsInitSystem
{
    public void Init(EcsSystems system)
    {
        EcsPool<HealthComponent> healthPool = system.GetWorld().GetPool<HealthComponent>();
        var filter = system.GetWorld().Filter<PlayerTag>().End();
        foreach (var entity in filter)
        {
            healthPool.Add(entity);
        }
    }

}