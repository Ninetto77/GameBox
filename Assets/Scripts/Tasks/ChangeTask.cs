using NaughtyAttributes;
using System;
using UnityEngine;
using Yandex;
using Zenject;

namespace Tasks
{
    public class ChangeTask : MonoBehaviour
    {
		public Action<int, int, int> OnKillEnemyTask;

		[Header("Номер задания")]
        [SerializeField] private int numberOfTask = -1;
		[Header("Тип смены задания")]
		[SerializeField] private ChangeTaskType changeTaskType;

		[Header("Активировать следующий коллайдер")]
		[SerializeField] private bool activateNextTaskCollider = false;
		[ShowIf("turnOnNextTaskCollider")]
		[SerializeField] private Collider nextTaskCollider;

		[Inject] private TaskManager taskManager;
		private bool isFirst = true;
		private EnemyKillTask enemyTask;

		private void Start()
		{
			isFirst = true;
			if (changeTaskType == ChangeTaskType.onEnemyKill)
				if (TryGetComponent(out EnemyKillTask enemyTask))
					enemyTask.OnEnemyKill += OnEnemyKill;

			if (changeTaskType == ChangeTaskType.onEndTask)
				if (TryGetComponent(out EnemyKillTask enemyTask))
					enemyTask.EndEnemyWave += EndEnemyWave;
		}

		private void EndEnemyWave()
		{
			taskManager.OnEndedTask?.Invoke(numberOfTask);
			SendMetricks();
			//Debug.Log("onEndTask");
		}

		private void OnEnemyKill(int curKillCount, int commonCount)
		{
			taskManager.OnEnemyKillTask?.Invoke(numberOfTask, curKillCount, commonCount);
			//Debug.Log("onEnemyKill");

		}

		private void OnTriggerEnter(Collider other)
		{
            if (!other.gameObject.CompareTag("Player")) return;
			
            if (!isFirst) return;

			if (changeTaskType == ChangeTaskType.onTriggerEnter)
			{
				taskManager.OnEndedTask?.Invoke(numberOfTask);
				SendMetricks();
			}

			if (activateNextTaskCollider)
				if (nextTaskCollider != null)
					nextTaskCollider.enabled = true;

			isFirst = !isFirst;

			//Debug.Log("onTriggerEnter " + gameObject.name);
		}

		private void OnDisable()
		{
			if (changeTaskType == ChangeTaskType.onDestroy)
				taskManager.OnEndedTask?.Invoke(numberOfTask);

			if (changeTaskType == ChangeTaskType.onEnemyKill)
				if (enemyTask != null)
					enemyTask.OnEnemyKill -= OnEnemyKill;

			if (changeTaskType == ChangeTaskType.onEndTask)
				if (enemyTask != null)
					enemyTask.EndEnemyWave -= EndEnemyWave;
		}

		private void SendMetricks()
		{
			Metrics.OnChangeTask(numberOfTask);
		}
	}

    [SerializeField]
    public enum ChangeTaskType
    {
        onTriggerEnter,
		onEnemyKill,
		onEndTask,
        onDestroy
    }
}
