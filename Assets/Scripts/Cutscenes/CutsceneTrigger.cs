using Cache;
using Sounds;
using System;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace CutScenes
{
	[RequireComponent(typeof(PlayableDirector))]
	public class CutsceneTrigger : MonoCache
	{
		[Header("Аудио для боссов")]
		[SerializeField] private bool playBossSound = false;

		[Header("Катсцена")]
		[SerializeField] private PlayableDirector playable;
		[Inject] private PlayerMoovement player;
		[Inject] private AudioManager audioManager;

		private Camera mainCamera;
		private bool IsFirstEnter = true; // это первый вход в триггер
		private const string bossSound = GlobalStringsVars.BOSS_SOUND_NAME;


		private void OnValidate()
		{
			playable = GetComponent<PlayableDirector>();
		}

		private void Start()
		{
			playable.playOnAwake = false;
			mainCamera = Camera.main;
		}

		public void PlayCutscene()
		{
			if (playable != null)
			{

				if (playBossSound)
					audioManager.PlaySound(bossSound);

				player.ChangeCanMoveState(false);
				mainCamera.gameObject.SetActive(false);
				playable.Play();
			}
		}

		public void StopCutscene()
		{
			if (playable != null)
			{
				Debug.Log("Stop");
				player.ChangeCanMoveState(true);
				mainCamera.gameObject.SetActive(true);
				playable.Stop();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!IsFirstEnter) return;
			if (!other.CompareTag("Player")) return;

			Debug.Log("Play " + gameObject.name + " " + other.name);
			PlayCutscene();
			IsFirstEnter = false;

		}
	}

	[Serializable]
	public struct CutsceneStruct
	{
		string keyCutscene;
		PlayableDirector playable;
	}
}