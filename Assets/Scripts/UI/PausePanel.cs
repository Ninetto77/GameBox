using Cache;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PausePanel : MonoCache
{
	[Header("AudioMixer")]
	public AudioMixerGroup Mixer;

	[Header("Audio snapshots")]
	public AudioMixerSnapshot Normal;
	public AudioMixerSnapshot InPause;

	[Header("UI")]
	public Toggle MusicToggle;
	public Toggle SFXToggle;

	private readonly string musicMixer = GlobalStringsVars.MUSICMIXER_NAME;
	private readonly string sfxMixer = GlobalStringsVars.SFXMIXER_NAME;

	private readonly string musicSave = GlobalStringsVars.MUSICSAVE_NAME;
	private readonly string sfxSave = GlobalStringsVars.SFXSAVE_NAME;

	private void Start()
	{
        if (MusicToggle != null)
			MusicToggle.isOn = PlayerPrefs.GetInt(musicSave, 1) == 1;

		if (SFXToggle != null)
			SFXToggle.isOn = PlayerPrefs.GetInt(sfxSave, 1) == 1;
	}

	private void OnEnable()
	{
		Time.timeScale = 0f;
		InPause.TransitionTo(0.5f);
	}

	private void OnDisable()
	{
		Time.timeScale = 1.0f;
		Normal.TransitionTo(0.5f);
	}

	public void ToggleMusic (bool state)
	{
		Mixer.audioMixer.SetFloat(musicMixer, state ? 0f : -80f);

		if (PlayerPrefs.HasKey(musicSave))
			PlayerPrefs.SetInt(musicSave, state ? 1 : 0);
	}

	public void ToggleSFX(bool state)
	{
		Mixer.audioMixer.SetFloat(sfxMixer, state ? 0f : -80f);

		if (PlayerPrefs.HasKey(sfxSave))
			PlayerPrefs.SetInt(sfxSave, state ? 1 : 0);
	}

	public void ChangeSFX(float volume)
	{
		Mixer.audioMixer.SetFloat(sfxMixer, Mathf.Lerp(-80, 0, volume));
	}
}
