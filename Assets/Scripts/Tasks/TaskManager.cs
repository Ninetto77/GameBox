using System;
using UnityEngine;
using Zenject;
using Languages;

namespace Tasks
{
	public class TaskManager : MonoBehaviour
	{
		public string[] RUTasks;
		public string[] ENTasks;

		public Action OnEndedTask;

		private int curTask = 0;
		private Language lang;
		[Inject] private UIManager uiManager;

		private void Start()
		{
			OnEndedTask += ChangeTask;
			curTask = 0;
		}

		/// <summary>
		/// Поменять задание
		/// </summary>
		public void ChangeTask()
		{
			uiManager.TaskText.text = RUTasks[curTask++];
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
