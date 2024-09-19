using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnDeadEvent;
    public Action<float> OnChangeHealth;
    public float MaxHealth;

    private float currentHealth;
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(currentHealth);
        currentHealth -= damage;
		OnChangeHealth?.Invoke(currentHealth);

        currentHealth = Math.Clamp(currentHealth, 0, MaxHealth);

		if (currentHealth <= 0)
            OnDeadEvent?.Invoke();
    }

    public void TakeTreat(float value)
    {
        currentHealth += value;
		OnChangeHealth?.Invoke(currentHealth);
	}

	public void RestoreHealth()
    {
        currentHealth = MaxHealth;
		OnChangeHealth?.Invoke(currentHealth);
	}

	public float GetCurrentHealth() => currentHealth;
}
