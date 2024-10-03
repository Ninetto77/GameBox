using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnDeadEvent;
    public Action<float> OnChangeHealth;
    public Action OnTakeDamage;
    public Action OnTakeTreat;
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

		if (currentHealth <= 0)
            OnDeadEvent?.Invoke();
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
