using Code.Global.Animations;
using System;
using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[Header("Canvases")]
	public CanvasGroup MainMenuCanvas;
	public CanvasGroup SettingsCanvas;
	public CanvasGroup LevelCanvas;

	[Header("Animation Settings")]
	public FadeAnimationPreset PresetFadeIn;
	public FadeAnimationPreset PresetFadeOut;
	public float TimeOfDisappear = 1.2f;

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

	private IEnumerator SetCanvas(CanvasGroup canvas)
	{
		AnimationShortCuts.FadeAnimation(curCanvas, PresetFadeOut);
		yield return new WaitForSeconds(TimeOfDisappear);
		AnimationShortCuts.FadeAnimation(canvas, PresetFadeIn);
		curCanvas = canvas;
	}
}

[Serializable]
public enum TypesCanvas
{
	mainMenu,
	settings,
	level,
}
