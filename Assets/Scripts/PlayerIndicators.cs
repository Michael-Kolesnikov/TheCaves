using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicators : MonoBehaviour
{
    public Slider healthBarSlider;
    public Slider staminaBarSlider;
    public Slider foodBarSlider;
    public Slider waterBarSlider;

    public Player player;
    public Gradient healthBarGradient;
    public Image healthBarFill;

    public float secondToEmptyFood = 30f;
    public float secondToEmptyWater = 15f;

    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            player.TakeDamage(10f);
        setHealth(player.CurrentHealth);
        SetStamina(player.CurrentStamina);
        SetFood();
        SetWater();
        
    }
    public void SetFood()
    {
        foodBarSlider.value -= secondToEmptyFood  * Time.deltaTime;
    }
    public void SetWater()
    {
        waterBarSlider.value -= secondToEmptyWater  * Time.deltaTime;
    }
    public void setHealth(float health)
    {
        healthBarSlider.value = health;
        healthBarFill.color = healthBarGradient.Evaluate(healthBarSlider.normalizedValue);
    }
    public void SetStamina(float stamina)
    {
        staminaBarSlider.value = stamina;

    }
}
