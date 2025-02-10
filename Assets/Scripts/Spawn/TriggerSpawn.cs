using Cache;
using Enemy;
using System;
using UnityEngine;
using Zenject;

public class TriggerSpawn : MonoCache
{
	[Header("Количество врагов")]
	public EnemyMarker[] enemyMarkers;

	[Inject] private IEnemyFactory enemyFactory;
	private bool IsFirstEnter = true; // это первый вход в триггер
	protected GameObject[] allChildren;

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

	protected void SpawnEnemies()
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
	protected void CreateEnemyMarkersArray()
	{
		int i = 0;

		if (allChildren.Length <= 0)
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
	protected void DestroyMarkers()
	{
		foreach (GameObject marker in allChildren)
		{
			Destroy(marker.gameObject);
		}
		Array.Clear(allChildren, 0, allChildren.Length);
	}
}
