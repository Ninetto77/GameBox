using Cache;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace CutScenes
{

	[RequireComponent(typeof(PlayableDirector))]
	public class CutsceneTrigger : MonoCache
	{
		[SerializeField] private PlayableDirector playable;
		//[SerializeField] private List<CutsceneStruct> cutscenes = new List<CutsceneStruct>();

		//public Dictionary<string, PlayableDirector> CutscenesDictionary = new Dictionary<string, PlayableDirector>();


		public void StartCutscene()
		{
			if (playable != null)
			{
				playable.Play();
			}
		}

		public void StopCutscene()
		{
			if (playable != null)
			{
				Debug.Log("Stop");
				playable.Stop();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player")) return;

			StartCutscene();
			//словарь катсцен: имя - объект таймлини
			//плэй катсцену
			//остановить катсцену
		}
	}

	[Serializable]
	public struct CutsceneStruct
	{
		string keyCutscene;
		PlayableDirector playable;
	}
}

