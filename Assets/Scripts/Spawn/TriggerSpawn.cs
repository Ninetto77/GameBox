using Enemy;
using UnityEngine;
using Zenject;

public class TriggerSpawn : MonoBehaviour
{
	public EnemyMarker[] enemyMarkers;

	[Inject] private IEnemyFactory enemyFactory;
	private bool IsFirstEnter = true; // это первый вход в триггер

	private void Start()
	{
		IsFirstEnter = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!IsFirstEnter) return;
		if (other == null) return;

		if (other.transform.CompareTag("Player"))
		{
			SpawnEnemies();
			IsFirstEnter = false;
		}
	}

	private void SpawnEnemies()
	{
		enemyFactory.Load();
		foreach (EnemyMarker marker in enemyMarkers)
			enemyFactory.Create(marker.EnemyType, marker.transform.position);
	}
}
