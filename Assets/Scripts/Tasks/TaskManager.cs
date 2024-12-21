using System;
using UnityEngine;
using Zenject;
using Languages;
using Sounds;

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
		[Inject] private AudioManager audioManager;
		private const string questSound = GlobalStringsVars.QUEST_SOUND_NAME;


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
			audioManager.PlaySound(questSound);
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
