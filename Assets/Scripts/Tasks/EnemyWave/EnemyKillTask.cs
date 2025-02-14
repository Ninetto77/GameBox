using Cache;
using Enemy;
using Enemy.States;
using System;
using System.Collections.Generic;
using Tasks;
using UnityEngine;
using Zenject;

public class EnemyKillTask : MonoCache
{
	public Action StartEnemyWave;
	public Action EndEnemyWave;

	[Header("Номер квеста")]
	public int numberOfTask = -1;

	[Header("Количество врагов для волн")]	
	public EnemyMarker[] enemyMarkers;
	public EnemyMarker[] enemyMarkers2;
	public EnemyMarker[] enemyMarkers3;


	[Header("Количество врагов для волн")]
	public Transform[] enemyMarkersTransforms = new Transform[3];

	[Inject] private IEnemyFactory enemyFactory;
	[Inject] private TaskManager taskManager;

	private int CountOfFirstWave;
	private int CountOfSecondWave;
	private int CountOfThirdWave;
	
	private bool IsFirstEnter = true; // это первый вход в триггер
	private int commonCount = 0;
    private int curKillCount = 0;
	
	private List<EnemyController> enemyControllers = new List<EnemyController>();
	protected List<GameObject> allChildren = new List<GameObject>();

	void Start()
    {
		IsFirstEnter = true;
		CountOfFirstWave = enemyMarkers.Length;
		CountOfSecondWave = CountOfFirstWave + enemyMarkers2.Length;
		CountOfThirdWave = CountOfSecondWave + enemyMarkers3.Length;
		
		commonCount = CountOfThirdWave;
	}

	protected override void OnTick()
	{
		base.OnTick();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (!IsFirstEnter) return;
		if (other == null) return;

		if (other.transform.CompareTag("Player"))
		{
			taskManager.OnKillEnemyTask?.Invoke(numberOfTask, curKillCount, commonCount);
			Spawn(1);
			StartEnemyWave?.Invoke();
			IsFirstEnter = false;
		}
	}

	private void CheckForWave()
	{
		curKillCount++;
		taskManager.OnKillEnemyTask?.Invoke(numberOfTask, curKillCount, commonCount);

		if (curKillCount == CountOfFirstWave)
		{
			Spawn(2);
		}
		else if (curKillCount == CountOfSecondWave)
		{
			Spawn(3);
		}
		else if (curKillCount == CountOfThirdWave)
		{
			EndEnemyWave?.Invoke();
			enemyControllers.Clear();
			Destroy(transform.gameObject, 5);
		}
	}

	/// <summary>
	/// Начать спавнить врагов
	/// </summary>
	/// <param name="numberOfWave"></param>
	private void Spawn(int numberOfWave)
	{
		CreateEnemyMarkersArray(numberOfWave);
		switch (numberOfWave)
		{
			case 1:
				SpawnEnemies(enemyMarkers);
				break;
			case 2:
				SpawnEnemies(enemyMarkers2);
				break;
			case 3:
				SpawnEnemies(enemyMarkers3);
				break;
			default:
				break;
		}
		CreateEnemyList();
		DestroyMarkers();
		Destroy(enemyMarkersTransforms[numberOfWave - 1].gameObject);
	}

	/// <summary>
	/// Заспавнить врагов
	/// </summary>
	/// <param name="enemies"></param>
	protected void SpawnEnemies(EnemyMarker[] enemies)
	{
		enemyFactory.Load();
		foreach (EnemyMarker marker in enemies)
			enemyFactory.Create(marker.type, marker.transform.position, transform);
	}

	/// <summary>
	/// Создать массив для уничтожения маркеров
	/// </summary>
	protected void CreateEnemyMarkersArray(int numberOfWave)
	{
		foreach (Transform child in enemyMarkersTransforms[numberOfWave - 1])
		{
			allChildren.Add(child.gameObject);
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
		allChildren.Clear();
	}

	/// <summary>
	/// Создать массив для добавления врагов, подписка на смерть
	/// </summary>
	protected void CreateEnemyList()
	{
		foreach (Transform child in transform)
		{
			var enemy = child.GetComponent<EnemyController>();
			if (enemy == null)
				continue;

			enemy.OnEnemyDeath += CheckForWave;
			enemyControllers.Add(enemy);
		}
	}
}
