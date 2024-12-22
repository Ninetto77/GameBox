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
			s.source.Play();
		}

        public void StopSound(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
			 s.source.Stop();
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
    }
}