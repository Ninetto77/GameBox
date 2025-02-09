using Cache;
using Enemy;
using UnityEngine;
using Zenject;

public class TriggerSpawn : MonoCache
{
	public EnemyMarker[] enemyMarkers;

	[Inject] private IEnemyFactory enemyFactory;
	private bool IsFirstEnter = true; // это первый вход в триггер
	private GameObject[] allChildren;

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
			//Destroy(this.gameObject);
		}
	}

	private void SpawnEnemies()
	{
		CreateEnemyMarkersArray();

		enemyFactory.Load();
		foreach (EnemyMarker marker in enemyMarkers)
			enemyFactory.Create(marker.type, marker.transform.position, transform);

		DestroyMarkers();
	}

	/// <summary>
	/// Создать массив для уничтожения маркеров
	/// </summary>
	private void CreateEnemyMarkersArray()
	{
		int i = 0;
		allChildren = new GameObject[transform.childCount];

		foreach (Transform child in transform)
		{
			allChildren[i] = child.gameObject;
			i += 1;
		}
	}

	/// <summary>
	/// Уничтожить маркеры
	/// </summary>
	private void DestroyMarkers()
	{
		foreach (GameObject marker in allChildren)
		{
			Destroy(marker.gameObject);
		}
	}
}
