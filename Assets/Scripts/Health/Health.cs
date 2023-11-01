using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth;

    private float currentHealth;
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void TakeTreat(float value)
    {
        currentHealth += value;
    }

    public void RestoreHealth()
    {
        currentHealth = MaxHealth;
    }

    public float GetCurrentHealth() => currentHealth;
}
