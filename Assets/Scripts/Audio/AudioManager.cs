using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Sounds
{
	public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup sfxMixerGroup;

		[SerializeField] private AudioSource musicSource;
		[SerializeField] private AudioSource SFXSource;

		public static AudioManager instance;
        private void Awake()
        {
            Initialize();
            PlaySound(GlobalStringsVars.MAIN_MUSIC_NAME);
		}

		public void Initialize()
        {
            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;

                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = sound.playOnAwake;
                if (sound.isSFX) sound.source.outputAudioMixerGroup = sfxMixerGroup;
                else sound.source.outputAudioMixerGroup = musicMixerGroup;
            }
        }

        public void PlaySound(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.Log("Sound: " + name + " not found!");
				return;
			}

            if (s.isSFX == true)
                SFXSource.PlayOneShot(s.clip);
            else
                musicSource.PlayOneShot(s.clip);
			// s.source.Play();
		}

        public void StopSound(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

			if (s.isSFX == true)
				SFXSource.Stop();
			else
				musicSource.Stop();
			// s.source.Stop();
		}

        public bool IsPlayingSound(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return false;
            }
            return s.source.isPlaying;
        }

        public float LengthSound(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return 0;
            }
            return s.clip.length;
        }

        /// <summary>
        /// Change all sounds volume exept one sound
        /// </summary>
        /// <param name="value">value of volume</param>
        /// <param name="name">sound name which doesnt need to be changed </param>
        public void ChangeAllSoundsVolume(float value, string name)
        {
            foreach (var sound in sounds)
            {
                if (sound.name != name)
                    sound.volume = value;
            }
        }

        public void ChangeAllSoundsVolume(float value)
        {
            foreach (var sound in sounds)
            {
                sound.volume = value;
            }
        }

        public void ChangeSoundVolume(string name, float value)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.volume = value;
        }
    }
}