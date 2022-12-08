using Leopotam.EcsLite;
using UnityEngine;

public sealed class PlayerUpdateHUDValuesSystem : IEcsRunSystem
{
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<PlayerStaminaUIComponent>().Inc<PlayerHealthUIComponent>().Inc<HealthComponent>().Inc<StaminaComponent>().End();

        foreach(var entity in filter)
        {
            // change stamina
            ref var stamina = ref system.GetWorld().GetPool<StaminaComponent>().Get(entity);
            ref var staminaSlider = ref system.GetWorld().GetPool<PlayerStaminaUIComponent>().Get(entity);
            staminaSlider.staminaSlider.value = stamina.currentStaminaValue;

            //change health
            ref var health = ref system.GetWorld().GetPool<HealthComponent>().Get(entity);
            ref var healthSlider = ref system.GetWorld().GetPool<PlayerHealthUIComponent>().Get(entity);
            healthSlider.healthSlider.value = health.currentHealthPoint;
        }
    }

}
