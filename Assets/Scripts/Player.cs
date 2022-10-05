using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float minHealth = 0f;
    public float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    internal void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
