using Cache;
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

	[Inject] private UIManager uIManager;

	private void Start()
	{
		isPause = false;
		curCanvas = GameCanvas;

		//убрать курсор
		SetCursorState(false);
	}
	protected override void OnTick()
	{
		if (uIManager.GetIsDead())
			return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SetSettings(PauseCanvas);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			SetSettings(TutorialCanvas);
		}
	}

	public void ContinueGame()
	{
		Time.timeScale = 1.0f;
		Normal.TransitionTo(0.5f);

		SetCanvas(GameCanvas);
		SetCursorState(false);

		isPause = !isPause;
	}

	private void PauseGame(Window curCanvas)
	{
		Time.timeScale = 0f;
		InPause.TransitionTo(0.5f);

		SetCanvas(curCanvas);
		SetCursorState(true);

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

	public bool GetIsPause() => isPause;

	private void SetCanvas(Window canvas)
	{
		canvas.Open_Instantly();
		curCanvas.Close_Instantly();
		curCanvas = canvas;
	}
}
