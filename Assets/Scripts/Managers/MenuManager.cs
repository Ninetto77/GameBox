using Code.Global.Animations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public Action<int> OnChangeLevelDescription;

	[Header("Canvases")]
	public CanvasGroup MainMenuCanvas;
	public CanvasGroup SettingsCanvas;
	public CanvasGroup LevelCanvas;

	[Header("Animation Settings")]
	public FadeAnimationPreset PresetFadeIn;
	public FadeAnimationPreset PresetFadeOut;
	public float TimeOfDisappear = 1.2f;

	[Header("Levels Description")]
	[Multiline]
	public string[] LevelDescription;
	public TextMeshProUGUI LevelDescriptionText;

	private CanvasGroup curCanvas;

	private void Start()
	{
		Time.timeScale = 1.0f;
		AnimationShortCuts.FadeAnimation(MainMenuCanvas, PresetFadeIn);
		AnimationShortCuts.FadeAnimation(SettingsCanvas, PresetFadeOut);
		AnimationShortCuts.FadeAnimation(LevelCanvas, PresetFadeOut);
		curCanvas = MainMenuCanvas;
	}

	public void SetMainMenu()
	{
		StartCoroutine(SetCanvas(MainMenuCanvas));
	}
	public void SetSetting()
	{
		StartCoroutine(SetCanvas(SettingsCanvas));
	}
	public void SetLevels()
	{
		StartCoroutine(SetCanvas(LevelCanvas));
	}

	public void SetLevelDescription(int numberLevel)
	{
		if (numberLevel < LevelDescription.Length && numberLevel > -1)
		{
			LevelDescriptionText.text = LevelDescription[numberLevel];
			OnChangeLevelDescription?.Invoke(numberLevel);
		}
		else
			Debug.Log($"There are no {numberLevel} Level Description");
	}

	private IEnumerator SetCanvas(CanvasGroup canvas)
	{
		AnimationShortCuts.FadeAnimation(curCanvas, PresetFadeOut);
		curCanvas.blocksRaycasts = false;

		yield return new WaitForSeconds(TimeOfDisappear);

		AnimationShortCuts.FadeAnimation(canvas, PresetFadeIn);
		
		curCanvas = canvas;
		canvas.blocksRaycasts = true;
	}
}

[Serializable]
public enum TypesCanvas
{
	mainMenu,
	settings,
	level,
}
