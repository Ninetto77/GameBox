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
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		isPause = !isPause;
	}

	private void PauseGame(Window curCanvas)
	{
		Time.timeScale = 0f;
		InPause.TransitionTo(0.5f);

		SetCanvas(curCanvas);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
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

	public bool GetIsPause() => isPause;

	private void SetCanvas(Window canvas)
	{
		canvas.Open_Instantly();
		curCanvas.Close_Instantly();
		curCanvas = canvas;
	}
}
