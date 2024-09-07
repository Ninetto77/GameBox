using UnityEngine;

public class HealthBar : Bar
{
	private Health health;

	public void Initialize(Health health)
	{
		this.health = health;
		if (health != null) health.OnChangeHealth += OnValueChanged;
	}

	private void OnDisable()
	{
		if (health != null) health.OnChangeHealth -= OnValueChanged;
	}
}
