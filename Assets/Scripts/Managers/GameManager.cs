using UnityEngine;
using CommonTimer;
using Zenject;
using Code.Global.Animations;
using System.Collections;

public class GameManager : MonoBehaviour
{
	//[SerializeField] private TimeBar bar;
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

    }
}
