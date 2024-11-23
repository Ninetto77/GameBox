using Cache;
using Sounds;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace PlayerScripts
{
	public class PlayerSound : MonoBehaviour
	{
		public GroundCheck groundCheck;

		[Inject] private AudioManager audioManager;
		private PlayerMoovement character;

		[Tooltip("Minimum velocity for moving audio to play")]
		public float velocityThreshold = .01f;
		Vector2 lastCharacterPosition;
		Vector2 CurrentCharacterPosition => new Vector2(character.transform.position.x, character.transform.position.z);

		private const string jumpSound = GlobalStringsVars.JUMP_SOUND_NAME;
		private const string runSound = GlobalStringsVars.RUN_SOUND_NAME;
		private const string walkSound = GlobalStringsVars.WALK_SOUND_NAME;

		private PlayerJump jump;
		private StatePlayerEnum curState;

		private void Awake()
		{
			character = GetComponent<PlayerMoovement>();
			groundCheck = GetComponentInChildren<GroundCheck>();
			//groundCheck = (transform.parent ?? transform).GetComponentInChildren<GroundCheck>();

			jump = GetComponent<PlayerJump>();
			curState = StatePlayerEnum.stay;

			//jump.Jumped += PlayJumpSound;
		}

		public void FixedUpdate()
		{
			float velocity = Vector3.Distance(CurrentCharacterPosition, lastCharacterPosition);

			if (velocity >= velocityThreshold && groundCheck && groundCheck.isGrounded)
			{
				if (character.IsRunning)
				{
					SetPlayingMovingAudio(StatePlayerEnum.run);
				}
				else
				{
					SetPlayingMovingAudio(StatePlayerEnum.walk);
				}
			}
			else
			{
				SetPlayingMovingAudio(StatePlayerEnum.stay);
			}

			lastCharacterPosition = CurrentCharacterPosition;
		}

		private void SetPlayingMovingAudio(StatePlayerEnum state)
		{
			try
			{
				if (curState == state)
					return;

				curState = state;
				switch (curState)
				{
					case StatePlayerEnum.stay:
						audioManager.StopSound(walkSound);
						audioManager.StopSound(runSound);
						break;
					case StatePlayerEnum.walk:
						audioManager.StopSound(runSound);
						audioManager.PlaySound(walkSound);
						break;
					case StatePlayerEnum.run:
						audioManager.StopSound(walkSound);
						audioManager.PlaySound(runSound);
						break;
					case StatePlayerEnum.jump:
						break;
					default:
						break;
				}

			}
			catch (Exception)
			{
				Debug.Log("Sound error");
			}
		}

		private void PlayJumpSound()
		{
			//Debug.Log("PlayShootSound");
			//try
			//{
			//	//audioManager.PlaySound();
			//}
			//catch (Exception)
			//{
			//	Debug.LogWarning("Sound error");
			//}
		}
	}
}