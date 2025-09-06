using Code.Global.Animations;
using SaveSystem;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yandex;
using Zenject;

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

	[Header("Levels Buttons")]
	public Button[] LevelButtons;

	[Header("Levels Description")]
	[Multiline]
	public string[] LevelDescription;
	public TextMeshProUGUI LevelDescriptionText;

	private CanvasGroup curCanvas;
	[Inject] private Progress progress;

	private void Start()
	{
		Time.timeScale = 1.0f;
		StartMainMenu();
		ChangeLevelButtons();
#if UNITY_WEBGL && !UNITY_EDITOR
		//��
		progress.LoadPlayerInfo();
#endif
	}

	/// <summary>
	/// Показать главное меню
	/// </summary>
	private void StartMainMenu()
	{
		AnimationShortCuts.FadeAnimation(MainMenuCanvas, PresetFadeIn);
		AnimationShortCuts.FadeAnimation(SettingsCanvas, PresetFadeOut);
		AnimationShortCuts.FadeAnimation(LevelCanvas, PresetFadeOut);
		curCanvas = MainMenuCanvas;

		Metrics.OnLoadedGame();
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
		curCanvas.blocksRaycasts = false;

		yield return new WaitForSeconds(TimeOfDisappear);

		AnimationShortCuts.FadeAnimation(canvas, PresetFadeIn);
		
		curCanvas = canvas;
		canvas.blocksRaycasts = true;

		SetLevelDescription(-1);
	}


	/// <summary>
	/// Показать описание уровня
	/// </summary>
	/// <param name="numberLevel"></param>
	public void SetLevelDescription(int numberLevel)
	{
		if (numberLevel < LevelDescription.Length && numberLevel > -1)
		{
			LevelDescriptionText.text = LevelDescription[numberLevel];
			OnChangeLevelDescription?.Invoke(numberLevel);
		}
		else
		{
			LevelDescriptionText.text = "";
			OnChangeLevelDescription?.Invoke(-1);
		}
	}

	/// <summary>
	/// Поменять видимость кнопок с уровнями
	/// </summary>
	private void ChangeLevelButtons()
	{
		int countOfOpenLevel = 1;
		countOfOpenLevel = progress.playerInfo.Level;

		switch (countOfOpenLevel)
		{
			case 1:
				SetInteractbleLevelButtons(1);
				break;
			case 2:
				SetInteractbleLevelButtons(2);
				break;
			case 3:
				SetInteractbleLevelButtons(3);
				break;
			default:
				SetInteractbleLevelButtons(1);
				break;
		}
	}

	/// <summary>
	/// Установить активность кнопок с уровнями
	/// </summary>
	/// <param name="count">Количество активных кнопок</param>
	private void SetInteractbleLevelButtons(int count)
	{
		for (int i = 0; i < count; i++)
		{
			LevelButtons[i].interactable = true;
			LevelButtons[i].GetComponent<Image>().color = Color.white;
		}
	}
}

[Serializable]
public enum TypesCanvas
{
	mainMenu,
	settings,
	level,
	none
}
