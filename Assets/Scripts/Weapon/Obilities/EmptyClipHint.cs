using Code.Global.Animations;
using System.Collections;
using UnityEngine;

namespace Hints
{
	public class EmptyClipHint : MonoBehaviour
	{
		[Header("Hints")]
		public CanvasGroup HintCanvas;

		[Header("Settings")]
		public float timeOfAppearHint = 0.8f;

		private bool isShowing;

		public void UpdateUI()
		{
			StartCoroutine(ShowHint());
		}

		private IEnumerator ShowHint()
		{
			if (!isShowing)
			{
				isShowing = true;
				AnimationShortCuts.FadeIn(HintCanvas);
				yield return new WaitForSeconds(timeOfAppearHint);
				AnimationShortCuts.FadeOut(HintCanvas);
				isShowing = false;
			}
		}
	}
}