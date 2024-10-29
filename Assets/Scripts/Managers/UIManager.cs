using Code.Global.Animations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[field: SerializeField] public Image ZoomIcon { get; set; }
	[field: SerializeField] public Image AimIcon { get ; set ; }
	[field: SerializeField] public TextMeshProUGUI TaskText { get; set; }

	[Header("RedOverlay/ Player Damage")]
	[Tooltip("Setting for fade in animation")]
	public FadeAnimationPreset FadeInRedOverlay;
	[Tooltip("Setting for fade out animation")]
	public FadeAnimationPreset FadeOutRedOverlay; 
	[SerializeField] private Image RedOverlay;

	[Header("Player Dead")]
	[Header("Canvas")]

	[SerializeField] private CanvasGroup DeadCanvas;
	[SerializeField] private CanvasGroup GameCanvas;

	[Header("Animations Dead")]

	[Tooltip("Setting for fade in animation")]
	public FadeAnimationPreset FadeInDeadCanvas;
	[Tooltip("Setting for fade out animation")]
	public FadeAnimationPreset FadeOutGameCanvas;

	[Header("Dead Canvas UI")]
	[SerializeField] private Image RestartButton;
	[SerializeField] private Image MenuButton;

	public Action OnPlayerDamage;
	public Action OnPlayerDead;

	private TextMeshProUGUI restartText;
	private TextMeshProUGUI menuText;

	private void Start()
	{
		OnPlayerDamage += ShowRedOverlay;
		OnPlayerDead += ShowDeadWindow;

		restartText = RestartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		menuText = MenuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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

}
