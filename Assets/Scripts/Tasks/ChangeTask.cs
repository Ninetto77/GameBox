using System;
using UnityEngine;
using Zenject;

namespace Tasks
{
    public class ChangeTask : MonoBehaviour
    {
		public Action<int, int, int> OnKillEnemyTask;

		[Header("Номер квеста")]
        [SerializeField] private int numberOfTask = -1;
		[Header("Тип переключения квеста")]
		[SerializeField] private ChangeTaskType changeTaskType;

        [Inject] private TaskManager taskManager;
		private bool isFirst = true;
		private EnemyKillTask enemyTask;

		private void Start()
		{
			isFirst = true;
			if (changeTaskType == ChangeTaskType.onEnemyKill 
				|| changeTaskType == ChangeTaskType.onEndTask)
				{ 
				if (TryGetComponent(out EnemyKillTask enemyTask))
				{
					enemyTask.OnEnemyKill += OnEnemyKill;
					enemyTask.EndEnemyWave += EndEnemyWave;
				}
			}
		}

		private void EndEnemyWave()
		{
			taskManager.OnEndedTask?.Invoke(numberOfTask);
		}

		private void OnEnemyKill(int curKillCount, int commonCount)
		{
			taskManager.OnEnemyKillTask?.Invoke(numberOfTask, curKillCount, commonCount);
		}

		private void OnTriggerEnter(Collider other)
		{
            if (!other.gameObject.CompareTag("Player")) return;
			
            if (!isFirst) return;

			if (changeTaskType == ChangeTaskType.onTriggerEnter)
				taskManager.OnEndedTask?.Invoke(numberOfTask);
			
            isFirst = !isFirst;
		}

		private void OnDisable()
		{
            if (changeTaskType == ChangeTaskType.onDestroy)
			    taskManager.OnEndedTask?.Invoke(numberOfTask);

			if (changeTaskType == ChangeTaskType.onEnemyKill
				|| changeTaskType == ChangeTaskType.onEndTask)
			{
				if (enemyTask != null)
				{
					enemyTask.OnEnemyKill -= OnEnemyKill;
					enemyTask.EndEnemyWave -= EndEnemyWave;
				}
			}
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
