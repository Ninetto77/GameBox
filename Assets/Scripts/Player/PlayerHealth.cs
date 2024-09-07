using UnityEngine;

public class PlayerHealth
{
    MonoBehaviour context;
	HealthBar bar;
    Health health;

	public PlayerHealth(HealthBar bar, Health health)
    {
        this.bar = bar;
        this.health = health;

        Init();
	}

    private void Init()
    {
        bar.Initialize(health);
	}
}
