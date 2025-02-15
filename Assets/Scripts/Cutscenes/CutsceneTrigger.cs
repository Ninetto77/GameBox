using Cache;
using System;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace CutScenes
{
	[RequireComponent(typeof(PlayableDirector))]
	public class CutsceneTrigger : MonoCache
	{
		[SerializeField] private PlayableDirector playable;
		[Inject] private PlayerMoovement player;
		//[SerializeField] private List<CutsceneStruct> cutscenes = new List<CutsceneStruct>();

		//public Dictionary<string, PlayableDirector> CutscenesDictionary = new Dictionary<string, PlayableDirector>();

		private void OnValidate()
		{
			playable = GetComponent<PlayableDirector>();
		}

		private void Start()
		{
			playable.playOnAwake = false;
		}

		public void PlayCutscene()
		{
			if (playable != null)
			{
				Debug.Log("Play");

				player.ChangeCanMoveState(false);
				playable.Play();
			}
		}

		public void StopCutscene()
		{
			if (playable != null)
			{
				Debug.Log("Stop");
				player.ChangeCanMoveState(true);
				playable.Stop();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player")) return;

			PlayCutscene();
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

