using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthbarSlider;
    public Gradient gradient;
    public Image fill;
    public Player player;
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            player.TakeDamage(10f);
        setHealth(player.CurrentHealth);
    }
    public void setHealth(float health)
    {
        healthbarSlider.value = health;
        fill.color = gradient.Evaluate(healthbarSlider.normalizedValue);
    }
}

