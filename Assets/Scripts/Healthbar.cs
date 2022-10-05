using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthbarSlider;
    public Gradient gradient;
    public Image fill;
    public Player player;
    public void setHealth(float health)
    {
        healthbarSlider.value = health;
        fill.color = gradient.Evaluate(healthbarSlider.normalizedValue);
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            player.TakeDamage(10f);
        setHealth(player.currentHealth);
    }
}
