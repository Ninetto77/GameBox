using System;
using UnityEngine;
using Zenject;
using Languages;
using Sounds;
using Points;

namespace Tasks
{
	public class TaskManager : MonoBehaviour
	{
		[TextArea]
		public string[] RUTasks;
		//public string[] ENTasks;

		public Action<int> OnEndedTask;
		public Action<int, int, int> OnEnemyKillTask;

		private Language lang;
		[Inject] private UIManager uiManager;
		[Inject] private AudioManager audioManager;

		private const string questSound = GlobalStringsVars.QUEST_SOUND_NAME;
		private const string killEnemyTask = " Убито ";

		private void Start()
		{
			OnEndedTask += ChangeTask;
			OnEnemyKillTask += ChangeTask;
		}

		/// <summary>
		/// Поменять задание
		/// </summary>
		public void ChangeTask(int number)
		{
			//uiManager.TaskText.text = RUTasks[curTask++];
			if (number == -1) return;
			uiManager.SetTaskUI(RUTasks[number]);
			audioManager.PlaySound(questSound);
		}

		/// <summary>
		/// Поменять задание
		/// </summary>
		public void ChangeTask(int number, int count, int commonCount)
		{
			string str = killEnemyTask + count + "/" + commonCount;
			if (number == -1) return;

			uiManager.SetTaskUI(RUTasks[number] + str);
			audioManager.PlaySound(questSound);
		}

		private void OnDestroy()
		{
			OnEndedTask -= ChangeTask;
		}

		///// <summary>
		///// Поменять задание
		///// </summary>
		//public void ChangeTask()
		//{
		//	if (lang.CurrentLanguage == "en")
		//		uiManager.TaskText.text = ENTasks[curTask++];
		//	else
		//	if (lang.CurrentLanguage == "ru")
		//		uiManager.TaskText.text = RUTasks[curTask++];

		//	else
		//		uiManager.TaskText.text = ENTasks[curTask++];

		//}
	}
}
