using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public Player player;
    private void Update()
    {
        SetStamina(player.CurrentStamina);
        Debug.Log(player.CurrentStamina);
    }
    public void SetStamina(float stamina)
    {
        staminaBar.value = stamina;

    }
}
