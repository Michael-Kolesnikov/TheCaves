using UnityEngine;
using Leopotam.EcsLite;
public class PlayerSendJumpEvent : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var playerFilter = system.GetWorld().Filter<PlayerTag>().End();

        var jumpEventPool = system.GetWorld().GetPool<JumpEvent>();

        if (!Input.GetKey(KeyCode.Space)) return;
        foreach(var entity in playerFilter)
        {
            jumpEventPool.Add(entity);
            Debug.Log("press space");

        }

    }
}
