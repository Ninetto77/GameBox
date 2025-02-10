using Cache;
using Enemy;
using System;
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

	[Inject] private IEnemyFactory enemyFactory;
	[Inject] private TaskManager taskManager;

	private int CountOfFirstWave;
	private int CountOfSecondWave;
	private int CountOfThirdWave;
	
	private bool IsFirstEnter = true; // это первый вход в триггер
	private int commonCount = 0;
    private int curKillCount = 0;
	
	protected GameObject[] allChildren;

	void Start()
    {
		IsFirstEnter = true;
		commonCount = CountOfFirstWave + CountOfSecondWave + CountOfThirdWave;
		CountOfFirstWave = enemyMarkers.Length;
		CountOfSecondWave = enemyMarkers2.Length;
		CountOfThirdWave = enemyMarkers3.Length;

		CreateEnemyMarkersArray();
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
			SpawnEnemies(enemyMarkers);
			StartEnemyWave?.Invoke();
			IsFirstEnter = false;
			//Destroy(this.gameObject);
		}
	}

	private void CheckForWave()
	{
		curKillCount++;
		if (curKillCount == CountOfFirstWave)
		{
			SpawnEnemies(enemyMarkers2);
		}
		else if (curKillCount == CountOfSecondWave)
		{
			SpawnEnemies(enemyMarkers3);
		}
		else if (curKillCount == CountOfThirdWave)
		{
			taskManager.OnEndedTask?.Invoke(numberOfTask);
			EndEnemyWave?.Invoke();
			DestroyMarkers();
		}

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
