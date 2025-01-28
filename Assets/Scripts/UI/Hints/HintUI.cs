using Code.Global.Animations;
using Points;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HintUI : MonoBehaviour
{
	public Action<TypeOfCartridge> OnAddCartridge;

	[Header("Type")]
	public TypeOfCartridge typeOfCartridge;

	[Header("Hints")]
	public CanvasGroup HintCanvas;

	[Header("Settings")]
	public float timeOfAppearHint = 0.8f;

	private int defaultText = 1;
	private const string PLUS_TEXT = "+";

	[Inject] private CartridgeShop shop;

	void Start()
	{
		shop.OnPickCartridge += UpdateUI;
	}

	private void UpdateUI(TypeOfCartridge type, int value)
	{
		if (typeOfCartridge == type)
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

public enum TypeOfCartridge
{
	light, heavy, oil, none
}
