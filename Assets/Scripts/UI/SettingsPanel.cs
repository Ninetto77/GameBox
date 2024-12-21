using Cache;
using Sounds;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SettingsPanel : MonoCache
{
	[Header("AudioMixer")]
	public AudioMixerGroup Mixer;

	[Header("Audio snapshots")]
	public AudioMixerSnapshot Normal;
	public AudioMixerSnapshot InPause;

	[Header("UI")]
	public Toggle MusicToggle;
	public Toggle SFXToggle;
	[Space]
	public Image MusicImage;
	public Image SFXTImage;

	private readonly string musicMixer = GlobalStringsVars.MUSICMIXER_NAME;
	private readonly string sfxMixer = GlobalStringsVars.SFXMIXER_NAME;

	private readonly string musicSave = GlobalStringsVars.MUSICSAVE_NAME;
	private readonly string sfxSave = GlobalStringsVars.SFXSAVE_NAME;
	
	private readonly string clickSound = GlobalStringsVars.CLICK_SOUND_NAME;

	[Inject] private AudioManager audioManager;

	private void Start()
	{
		if (MusicToggle != null)
		{
			MusicToggle.isOn = (PlayerPrefs.GetInt(musicSave, 1) == 1);
			if (MusicImage != null)
				MusicImage.enabled = MusicToggle.isOn;
		}

		if (SFXToggle != null)
		{
			SFXToggle.isOn = (PlayerPrefs.GetInt(sfxSave, 1) == 1);
			if (SFXTImage != null)
				SFXTImage.enabled = SFXToggle.isOn;
		}

		PlayerPrefs.SetInt(musicSave, MusicToggle.isOn ? 1 : 0);
		PlayerPrefs.SetInt(sfxSave, SFXToggle.isOn ? 1 : 0);
	}
	public void ToggleMusic (bool state)
	{
		Mixer.audioMixer.SetFloat(musicMixer, state ? 0f : -80f);

		if (PlayerPrefs.HasKey(musicSave))
			PlayerPrefs.SetInt(musicSave, state ? 1 : 0);

		if (MusicImage != null)
			MusicImage.enabled = state;
		PlaySoundClick();
	}

	public void ToggleSFX(bool state)
	{
		Mixer.audioMixer.SetFloat(sfxMixer, state ? 0f : -80f);

		if (PlayerPrefs.HasKey(sfxSave))
			PlayerPrefs.SetInt(sfxSave, state ? 1 : 0);

		if (SFXTImage != null)
			SFXTImage.enabled = state;
		PlaySoundClick();
	}


	private void PlaySoundClick()
	{
		audioManager.PlaySound(clickSound);
	}

	public void ChangeSFX(float volume)
	{
		Mixer.audioMixer.SetFloat(sfxMixer, Mathf.Lerp(-80, 0, volume));
	}
}
