using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnDeadEvent;
    public Action OnChangeHealth;
    public float MaxHealth;

    private float currentHealth;
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
		OnChangeHealth?.Invoke();

		if (currentHealth <= 0)
            OnDeadEvent?.Invoke();
    }

    public void TakeTreat(float value)
    {
        currentHealth += value;
		OnChangeHealth?.Invoke();
	}

	public void RestoreHealth()
    {
        currentHealth = MaxHealth;
		OnChangeHealth?.Invoke();
	}

	public float GetCurrentHealth() => currentHealth;
}
