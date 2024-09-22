using UnityEngine;
using Zenject;

public class PlayerHealth: MonoBehaviour
{
	private HealthBar bar;
	private Health health;

	[Inject]
	private void Construct(PlayerMoovement player)
	{
		health = player.health;
	}

	private void Start()
	{
		bar = GetComponent<HealthBar>();
		Init();
	}

	private void Init()
    {
        bar.Initialize(health);
	}
	//public PlayerHealth(HealthBar bar, Health health)
	//   {
	//       this.bar = bar;
	//       this.health = health;

	//       Init();
	//}
}
