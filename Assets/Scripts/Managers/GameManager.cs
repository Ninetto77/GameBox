using UnityEngine;
using CommonTimer;
using Zenject;
using Code.Global.Animations;
using System.Collections;

public class GameManager : MonoBehaviour
{

	[SerializeField] private TimeBar bar;
	[Inject] private UIManager uiManager;
	private void Awake()
	{
		//Timer timer = new Timer(this);

		//bar.Initialize(timer);
		//timer.SetTimer(5);
		//timer.StartCountingTime();
	}
	void Start()
    {
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		StartCoroutine(ShowTutorialHint());
    }

	private IEnumerator ShowTutorialHint()
	{
		AnimationShortCuts.FadeIn(uiManager.HintTutorialText);
		yield return new WaitForSeconds(2f);
		AnimationShortCuts.FadeOut(uiManager.HintTutorialText);
	}
}
