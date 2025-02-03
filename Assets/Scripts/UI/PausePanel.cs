using Cache;
using Code.Global.Animations;
using Sounds;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class PausePanel : MonoCache
{
	[Header("Canvases")]
	public Window PauseCanvas;
	public Window TutorialCanvas;
	public Window GameCanvas;

	[Header("Cursor")]
	public Texture2D cursorTexture;

	[Header("Audio snapshots")]
	public AudioMixerSnapshot Normal;
	public AudioMixerSnapshot InPause;

	private bool isPause;
	private Window curCanvas;

	private const string tutorialSound = GlobalStringsVars.TUTORIAL_SOUND_NAME;
	private const string runSound = GlobalStringsVars.RUN_SOUND_NAME;
	private const string walkSound = GlobalStringsVars.WALK_SOUND_NAME;

	[Inject] private UIManager uiManager;
	[Inject] private AudioManager audioManager;

	private void Start()
	{
		isPause = false;
		curCanvas = GameCanvas;

		//убрать курсор
		SetCursorState(false);
		//показать подсказку о туториале
		ShowTutorialHint();
	}
	protected override void OnTick()
	{
		if (uiManager.GetIsDead())
			return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SetSettings(PauseCanvas);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			SetSettings(TutorialCanvas);
			audioManager.PlaySound(tutorialSound);
		}
	}

	/// <summary>
	/// продолжить игру
	/// </summary>
	public void ContinueGame()
	{
		Time.timeScale = 1.0f;
		Normal.TransitionTo(0.5f);

		SetCanvas(GameCanvas);
		SetCursorState(false);

		isPause = !isPause;
	}

	/// <summary>
	/// поставить на паузу
	/// </summary>
	/// <param name="curCanvas"></param>
	private void PauseGame(Window curCanvas)
	{
		Time.timeScale = 0f;
		InPause.TransitionTo(0.5f);

		SetCanvas(curCanvas);
		SetCursorState(true);

		audioManager.StopSound(walkSound);
		audioManager.StopSound(runSound);

		isPause = !isPause;
	}

	private void SetSettings(Window curCanvas)
	{
		if (isPause)
		{
			ContinueGame();
		}
		else
		{
			PauseGame(curCanvas);
		}
	}

	/// <summary>
	/// Установить курсор (не)видимым
	/// </summary>
	/// <param name="state"></param>
	private void SetCursorState(bool state)
	{
		if (state)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
		}
        else
        {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
    }

	/// <summary>
	/// показать подсказку о туториале
	/// </summary>
	private void ShowTutorialHint()
	{
		AnimationShortCuts.Blink(uiManager.HintTutorialText);
	}

	public bool GetIsPause() => isPause;

	/// <summary>
	/// установить канвас
	/// </summary>
	/// <param name="canvas"></param>
	private void SetCanvas(Window canvas)
	{
		canvas.Open_Instantly();
		curCanvas.Close_Instantly();
		curCanvas = canvas;
	}
}
