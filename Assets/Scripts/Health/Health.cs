using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action<float> OnChangeHealth;
    public event Action OnTakeDamage;
    public event Action OnTakeTreat;
    public float MaxHealth;

    private float currentHealth;
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
		OnChangeHealth?.Invoke(currentHealth);
		OnTakeDamage?.Invoke();

        currentHealth = Math.Clamp(currentHealth, 0, MaxHealth);
    }

    public void TakeTreat(float value)
    {
        currentHealth += value;
		
        currentHealth = Math.Clamp(currentHealth, 0, MaxHealth);

		OnChangeHealth?.Invoke(currentHealth);
		OnTakeTreat?.Invoke();

	}

	public void RestoreHealth()
    {
        currentHealth = MaxHealth;
		OnChangeHealth?.Invoke(currentHealth);
	}

	public float GetCurrentHealth() => currentHealth;
}
