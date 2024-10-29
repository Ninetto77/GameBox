using Cache;
using UnityEngine;
using UnityEngine.Audio;

public class PausePanel : MonoCache
{
	[Header("Canvases")]
	public Window PauseCanvas;
	public Window GameCanvas;

	[Header("Audio snapshots")]
	public AudioMixerSnapshot Normal;
	public AudioMixerSnapshot InPause;
	
	private bool isPause;
	private Window curCanvas;

	private void Start()
	{
		isPause = false;
		curCanvas = GameCanvas;
	}
	protected override void OnTick()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SetSettings();
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

	private void PauseGame()
	{
		Time.timeScale = 0f;
		InPause.TransitionTo(0.5f);

		SetCanvas(PauseCanvas);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		isPause = !isPause;

	}

	private void SetSettings()
	{
		if (isPause)
		{
			ContinueGame();
		}
		else
		{
			PauseGame();
		}

	}



	private void SetCanvas(Window canvas)
	{
		canvas.Open_Instantly();
		curCanvas.Close_Instantly();
		curCanvas = canvas;
	}
}
