using Cache;
using Enemy;
using Enemy.States;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Tasks
{
	public class EnemyKillTask : MonoCache
	{
		public Action StartEnemyWave;
		public Action EndEnemyWave;
		public Action<int, int> OnEnemyKill;

		[Header("Enemy markers")]
		[SerializeField] private EnemyMarker[] enemyMarkers;
		[SerializeField] private EnemyMarker[] enemyMarkers2;
		[SerializeField] private EnemyMarker[] enemyMarkers3;

		[Header("Transforms with markers in each wave")]
		[SerializeField] private Transform[] enemyMarkersTransforms = new Transform[3];

		[Inject] private IEnemyFactory enemyFactory;
		[Inject] private TaskManager taskManager;

		private int CountOfFirstWave;
		private int CountOfSecondWave;
		private int CountOfThirdWave;

		private bool IsFirstEnter = true; // ��� ������ ���� � �������
		private int commonCount = 0;
		private int curKillCount = 0;

		protected List<GameObject> allChildren = new List<GameObject>();

		private List<EnemyController> enemyControllers = new List<EnemyController>();
		private Dictionary<string, Action> enemySubscriptions = new Dictionary<string, Action>();

		int i = 0;

		void Start()
		{
			IsFirstEnter = true;
			CountOfFirstWave = enemyMarkers.Length;
			CountOfSecondWave = CountOfFirstWave + enemyMarkers2.Length;
			CountOfThirdWave = CountOfSecondWave + enemyMarkers3.Length;

			commonCount = CountOfThirdWave;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!IsFirstEnter) return;
			if (other == null) return;

			if (other.transform.CompareTag("Player"))
			{
				Spawn(1);
				StartEnemyWave?.Invoke();
				IsFirstEnter = false;
			}
		}

		private void CheckForWave(EnemyController enemy)
		{
			curKillCount++;

			if (curKillCount != CountOfThirdWave)
				OnEnemyKill?.Invoke(curKillCount, commonCount);

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
				Destroy(transform.gameObject, 5);
			}

			ClearEnemyList(enemy);
		}

		private void ClearEnemyList(EnemyController enemy)
		{
			// ������������ �� ������� ��� ����������� �����
			if (enemySubscriptions.TryGetValue(enemy.UniqueId, out var subscription))
			{
				enemy.OnEnemyDeath -= subscription;
				enemySubscriptions.Remove(enemy.UniqueId);
			}
			enemyControllers.Remove(enemy);
		}

		/// <summary>
		/// ������ �������� ������
		/// </summary>
		/// <param name="numberOfWave"></param>
		private void Spawn(int numberOfWave)
		{
			CreateMarkersArray(numberOfWave);
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
			DestroyMarkers();
			Destroy(enemyMarkersTransforms[numberOfWave - 1].gameObject);
		}

		/// <summary>
		/// ���������� ������
		/// </summary>
		/// <param name="enemies"></param>
		protected void SpawnEnemies(EnemyMarker[] enemies)
		{
			enemyFactory.Load();
			foreach (EnemyMarker marker in enemies)
			{
				var temp = enemyFactory.Create(marker.type, marker.transform.position);
				CreateEnemyList(temp);
			}
		}

		/// <summary>
		/// ������� ������ ��� ���������� ������, �������� �� ������
		/// </summary>
		private void CreateEnemyList(GameObject temp)
		{
			if (temp.TryGetComponent(out EnemyController enemy))
			{
				if (!enemySubscriptions.ContainsKey(enemy.UniqueId))
				{
					Action subscription = () => CheckForWave(enemy);
					enemy.OnEnemyDeath += subscription;
					enemySubscriptions[enemy.UniqueId] = subscription;
				}
				enemyControllers.Add(enemy);
			}
		}

		/// <summary>
		/// ������� ������ ��� ����������� ��������
		/// </summary>
		protected void CreateMarkersArray(int numberOfWave)
		{
			foreach (Transform child in enemyMarkersTransforms[numberOfWave - 1])
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
			allChildren.Clear();
		}

	}
}
