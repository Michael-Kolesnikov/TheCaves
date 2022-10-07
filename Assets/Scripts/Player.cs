using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float minHealth = 0;
    private float _currentHealth;
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth= Math.Max(value,0);
        }
    }
    public float maxStamina = 100f;
    public float minStamina = 100f;
    public float _currentStamina;
    public float CurrentStamina
    {
        get { return _currentStamina; }
        set
        {
            _currentStamina= Math.Max(value,0);
        }
    }
    void Start()
    {
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
    }
    private void Update()
    {
        Debug.Log(CurrentHealth);
    }
    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;
    }
    public void DecreaseStamina(float deltaStamina)
    {
        CurrentStamina -= deltaStamina;
    }
}
