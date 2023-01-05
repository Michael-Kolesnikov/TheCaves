
/* Необъединенное слияние из проекта "Scripts.Player"
До:
using UnityEngine;
using Leopotam.EcsLite;
После:
using Leopotam.EcsLite;
using UnityEngine;
*/
using Leopotam.EcsLite;

public sealed class InitChunkStoreSystem : IEcsInitSystem
{
    public void Init(EcsSystems system)
    {
    }
}
