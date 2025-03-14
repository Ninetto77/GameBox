using Cache;
using Code.Global.Animations;
using Sounds;
using System.Runtime.InteropServices;
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

	//������� � ������ ����
	[DllImport("__Internal")]
	private static extern void SetStartGameplayAPI();	

	//������� �� ��������� ����
	[DllImport("__Internal")]
	private static extern void SetStopGameplayAPI();

	private void Start()
	{
#if UNITY_WEBGL
		//��
		//SetStartGameplayAPI();
#endif
		isPause = false;
		curCanvas = GameCanvas;

		//������ ������
		SetCursorState(false);
		//�������� ��������� � ���������
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
	/// ���������� ����
	/// </summary>
	public void ContinueGame()
	{
#if UNITY_WEBGL
		//��
		//SetStartGameplayAPI();
#endif
		Time.timeScale = 1.0f;
		Normal.TransitionTo(0.5f);

		SetCanvas(GameCanvas);
		SetCursorState(false);

		isPause = !isPause;
	}

	/// <summary>
	/// ��������� �� �����
	/// </summary>
	/// <param name="curCanvas"></param>
	private void PauseGame(Window curCanvas)
	{
#if UNITY_WEBGL
		//��
		//SetStopGameplayAPI();
#endif
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
	/// ���������� ������ (��)�������
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
	/// �������� ��������� � ���������
	/// </summary>
	private void ShowTutorialHint()
	{
		AnimationShortCuts.Blink(uiManager.HintTutorialText, 20, 1);
	}

	public bool GetIsPause() => isPause;

	/// <summary>
	/// ���������� ������
	/// </summary>
	/// <param name="canvas"></param>
	private void SetCanvas(Window canvas)
	{
		canvas.Open_Instantly();
		curCanvas.Close_Instantly();
		curCanvas = canvas;
	}
}
