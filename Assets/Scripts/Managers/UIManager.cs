using Code.Global.Animations;
using DG.Tweening;
using Points;
using Sounds;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
	[field: SerializeField] public Image ZoomIcon { get; set; }
	[field: SerializeField] public Image AimIcon { get ; set ; }
	[field: SerializeField] public TextMeshProUGUI TaskText { get; private set; }
	[field: SerializeField] public TextMeshProUGUI HintTutorialText { get; set; }

	[Header("RedOverlay/ Player Damage")]
	[Tooltip("Setting for fade in animation")]
	public FadeAnimationPreset FadeInRedOverlay;
	[Tooltip("Setting for fade out animation")]
	public FadeAnimationPreset FadeOutRedOverlay; 
	[SerializeField] private Image RedOverlay;

	[Header("Player Dead")]
	[Header("Canvas")]

	[SerializeField] private CanvasGroup DeadCanvas;
	[SerializeField] private CanvasGroup WinCanvas;
	[SerializeField] private CanvasGroup GameCanvas;

	[Header("Animations Dead")]

	[Tooltip("Setting for fade in animation")]
	public FadeAnimationPreset FadeInDeadCanvas;
	[Tooltip("Setting for fade out animation")]
	public FadeAnimationPreset FadeOutGameCanvas;

	[Header("Dead Canvas UI")]
	[SerializeField] private Image RestartButton;
	[SerializeField] private Image MenuButton;

	[Header("Win Canvas UI")]
	[SerializeField] private Image CandyImage;
	[SerializeField] private Image MenuWinButton;
	[SerializeField] private TextMeshProUGUI CandyText;
	[SerializeField] private TextMeshProUGUI PointText;

	public Action OnPlayerDamage;
	public Action OnPlayerDead;
	public Action OnPlayerWin;

	private TextMeshProUGUI restartText;
	private TextMeshProUGUI menuText;
	private TextMeshProUGUI menuWinText;

	[Inject] private PointsLevel shop;
	private bool isDead;

	private const string candyTextCount = "Количество собранных конфет: ";
	private const string pointTextCount = "Количество очков: ";

	private void Start()
	{
		OnPlayerDamage += ShowRedOverlay;
		OnPlayerDead += ShowDeadWindow;
		OnPlayerWin += ShowWinWindow;

		restartText = RestartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		menuText = MenuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

		menuWinText = MenuWinButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

		isDead = false;
	}


	#region Damage Player

	private void ShowRedOverlay()
	{
		StartCoroutine(ShowRedOverlayForTime());
	}

	private IEnumerator ShowRedOverlayForTime()
	{
		AnimationShortCuts.FadeAnimation(RedOverlay, FadeInRedOverlay);
		yield return new WaitForSeconds(0.5f);
		AnimationShortCuts.FadeAnimation(RedOverlay, FadeOutRedOverlay);
	}
	#endregion

	#region Dead Player
	private void ShowDeadWindow()
	{
		StartCoroutine(ShowDeadWindowForTime());
	}

	private IEnumerator ShowDeadWindowForTime()
	{
		isDead = true;
			
		AnimationShortCuts.FadeAnimation(GameCanvas, FadeOutGameCanvas);
		GameCanvas.blocksRaycasts = false;
		DeadCanvas.interactable = false;


		yield return new WaitForSeconds(0.5f);
		AnimationShortCuts.FadeAnimation(DeadCanvas, FadeInDeadCanvas);
		DeadCanvas.blocksRaycasts = true;
		DeadCanvas.interactable = true;


		yield return new WaitForSeconds(2f);
		AnimationShortCuts.FadeIn(RestartButton);
		AnimationShortCuts.FadeIn(restartText);
		yield return new WaitForSeconds(1f);
		AnimationShortCuts.FadeIn(MenuButton);
		AnimationShortCuts.FadeIn(menuText);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		yield return new WaitForSeconds(0.5f);

		//Time.timeScale = 0f;
	}
	#endregion

	#region Win Player
	private void ShowWinWindow()
	{
		StartCoroutine(ShowWinWindowForTime());
	}

	private IEnumerator ShowWinWindowForTime()
	{
		isDead = true;
		CandyText.text = candyTextCount + shop.countOfCandy;
		PointText.text = pointTextCount + shop.curPoints.Value;

		AnimationShortCuts.FadeAnimation(GameCanvas, FadeOutGameCanvas);
		GameCanvas.blocksRaycasts = false;
		GameCanvas.interactable = false;


		yield return new WaitForSeconds(0.5f);
		AnimationShortCuts.FadeAnimation(WinCanvas, FadeInDeadCanvas);
		WinCanvas.blocksRaycasts = true;
		WinCanvas.interactable = true;


		yield return new WaitForSeconds(2f);
		AnimationShortCuts.FadeIn(CandyImage);
		AnimationShortCuts.FadeIn(CandyText);

		yield return new WaitForSeconds(1f);
		AnimationShortCuts.FadeIn(PointText);

		yield return new WaitForSeconds(1f);
		AnimationShortCuts.FadeIn(MenuWinButton);
		AnimationShortCuts.FadeIn(menuWinText);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		yield return new WaitForSeconds(0.5f);

		//Time.timeScale = 0f;
	}

	#endregion

	#region Задания квеста
	public void SetTaskUI(string task)
	{
		TaskText.text = task;
		AnimationShortCuts.PopEffect(TaskText, 1.1f, 0.5f).SetLoops(10) ;
		
	}
	#endregion

	public bool GetIsDead() => isDead;

	private void OnDestroy()
	{
		OnPlayerDamage -= ShowRedOverlay;
		OnPlayerDead -= ShowDeadWindow;
		OnPlayerWin -= ShowWinWindow;
	}

}
