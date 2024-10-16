using Code.Global.Animations;
using Points;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class CandyUI : MonoBehaviour
{
	[Inject] private ShopPoint shop;
	
	[Header("Candy Info")]
	public TextMeshProUGUI CandyText;

	[Header("Candy Hint")]
	public CanvasGroup CandyHintCanvas;
	public FadeAnimationPreset CandyHintPresetFade;
	public TextMeshProUGUI CandyHintText;
	public float timeOfAppearHint = 0.8f;

	private int defaultText = 1;
	private const string PLUS_TEXT = "+";
	void Start()
    {
        shop.OnChangedPoints += UpdateUI;
	}

	private void UpdateUI(int values)
	{
		CandyText.text = values.ToString();
		StartCoroutine(ShowHint());
	}

	private IEnumerator ShowHint()
	{
		if (CandyHintCanvas.alpha != 0)
			defaultText += 1;
		else
			defaultText = 1;

		CandyHintText.text = PLUS_TEXT + defaultText.ToString();
		AnimationShortCuts.FadeIn(CandyHintCanvas);
		yield return new WaitForSeconds(timeOfAppearHint);
		AnimationShortCuts.FadeOut(CandyHintCanvas);
	}
}
