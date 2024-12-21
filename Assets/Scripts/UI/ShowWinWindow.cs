using Enemy.States;
using Sounds;
using UnityEngine;
using Zenject;
namespace Enemy
{
	public class ShowWinWindow : MonoBehaviour
	{
		[Inject] private UIManager uiManager;
		[Inject] private AudioManager audioManager;

		private const string winSound = GlobalStringsVars.WIN_SOUND_NAME;
		private EnemyController enemyController;



		private void Start()
		{
			enemyController = GetComponent<EnemyController>();
			if (enemyController != null )
				enemyController.OnEnemyDeath += ShowWinWindowFunc;
		}

		private void ShowWinWindowFunc()
		{
			uiManager.OnPlayerWin?.Invoke();
			audioManager.PlaySound(winSound);
		}
	}
}
