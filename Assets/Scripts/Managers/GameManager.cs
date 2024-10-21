using UnityEngine;
using CommonTimer;

public class GameManager : MonoBehaviour
{
	[SerializeField]private TimeBar bar;
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
    }
}
