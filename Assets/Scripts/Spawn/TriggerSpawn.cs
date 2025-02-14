using Cache;
using Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerSpawn : MonoCache
{
	[Header("���������� ������")]
	public EnemyMarker[] enemyMarkers;

	[Inject] private IEnemyFactory enemyFactory;
	private bool IsFirstEnter = true; // ��� ������ ���� � �������
	private List<GameObject> allChildren = new List<GameObject>();

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
	/// ������� ������ ��� ����������� ��������
	/// </summary>
	protected void CreateEnemyMarkersArray()
	{
		foreach (Transform child in transform)
		{
			allChildren.Add(child.gameObject);
		}
	}

	/// <summary>
	/// ���������� �������
	/// </summary>
	protected void DestroyMarkers()
	{
		foreach (GameObject marker in allChildren)
		{
			Destroy(marker.gameObject);
		}
	}
}
