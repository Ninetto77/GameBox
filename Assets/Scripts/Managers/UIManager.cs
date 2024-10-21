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
	public FadeAnimationPreset FadeIn;
	[Tooltip("Setting for fade out animation")]
	public FadeAnimationPreset FadeOut; 
	[SerializeField] private Image RedOverlay;


	public Action OnPlayerDamage;

	private void Start()
	{
		OnPlayerDamage += ShowRedOverlay;
	}
	public void ShowRedOverlay()
	{
		StartCoroutine(ShowRedOverlayForTime());
	}

	private IEnumerator ShowRedOverlayForTime()
	{
		AnimationShortCuts.FadeAnimation(RedOverlay, FadeIn);
		yield return new WaitForSeconds(0.5f);
		AnimationShortCuts.FadeAnimation(RedOverlay, FadeOut);
	}
}
