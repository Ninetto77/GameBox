using Code.Global.Animations;
using Points;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HintUI : MonoBehaviour
{
	[Header("Hints")]
	public CanvasGroup HintCanvas;
	public FadeAnimationPreset HintPresetFade;
	public Image HintImage;
	public TextMeshProUGUI HintText;
	public float timeOfAppearHint = 0.8f;

	private int defaultText = 1;
	private const string PLUS_TEXT = "+";

	[Inject] private ShopPoint shop;

	void Start()
	{
		shop.OnAddHint += UpdateUI;
	}

	private void UpdateUI()
	{
		StartCoroutine(ShowHint());
	}

	private IEnumerator ShowHint()
	{
		if (HintCanvas.alpha != 0)
			defaultText += 1;
		else
			defaultText = 1;

		AnimationShortCuts.FadeIn(HintCanvas);
		yield return new WaitForSeconds(timeOfAppearHint);
		AnimationShortCuts.FadeOut(HintCanvas);
	}
}
