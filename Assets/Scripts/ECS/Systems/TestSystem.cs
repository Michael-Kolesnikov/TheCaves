using UnityEngine;
using Leopotam.EcsLite;

/// <summary>
/// рнкэйн дкъ реярхпнбюмхъ дкъ янаябреммнцн хяонкэгнбюмхъ
/// </summary>
public sealed class TestSystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var poolWhoHasHealthPoint =  system.GetWorld().GetPool<PlayerTag>();
        var healthPointPool =  system.GetWorld().GetPool<HealthComponent>();
        var playerFilter = system.GetWorld().Filter<PlayerTag>().End();
        foreach (var entity in playerFilter)
        {
            ref var playerHealth = ref healthPointPool.Get(entity);
            playerHealth.currentHealthPoint++;
        }
    }
}
