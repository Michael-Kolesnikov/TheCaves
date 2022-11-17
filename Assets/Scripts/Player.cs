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
            _currentHealth= Math.Max(value,minHealth);
        }
    }
    public float maxStamina = 100f;
    public float minStamina = 0;

    public float staminaRecoveryPerSecond = 1f;

    public float _currentStamina;
    public float CurrentStamina
    {
        get { return _currentStamina; }
        set
        {
            if(value > maxStamina)
                _currentStamina = maxStamina;
            else
                _currentStamina = Math.Max(value, minStamina);
            
        }
    }
    private float _currentFood;
    public float CurrentFood
    {
        get { return _currentFood; }
        set
        {
            _currentFood = Mathf.Max(value, 0);
        }
    }
    private float _currentWater;
    public float CurrentWater
    {
        get { return _currentWater; }
        set
        {
            _currentWater = Mathf.Max(value, 0);
        }
    }
    void Start()
    {
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
    }
    private void Update()
    {
        StartCoroutine(RecoveryStamina());
    }
    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;
    }
    public void DecreaseStamina(float deltaStamina)
    {
        CurrentStamina -= deltaStamina;
    }
    public IEnumerator RecoveryStamina()
    {
        yield return new WaitForSeconds(1);
        CurrentStamina += 0.07f;
    }
 
}
