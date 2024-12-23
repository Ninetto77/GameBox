using Enemy.States;
using Sounds;
using UnityEngine;
using Zenject;
using SaveSystem;
using Points;

namespace Enemy.Abilities
{
	public class ShowWinWindow : MonoBehaviour
	{
		[Inject] private UIManager uiManager;
		[Inject] private AudioManager audioManager;
		[Inject] private ShopPoint shop;

		private const string winSound = GlobalStringsVars.WIN_SOUND_NAME;
		private const string musicSound = GlobalStringsVars.MAIN_MUSIC_NAME;

		private EnemyController enemyController;

		private void Start()
		{
			enemyController = GetComponent<EnemyController>();

#if UNITY_WEBGL
			Progress.instance.playerInfo.Level++;
			Progress.instance.playerInfo.Points = shop.curPoints.Value;
			Progress.instance.SavePlayerInfo();
#endif

			if (enemyController != null )
				enemyController.OnEnemyDeath += ShowWinWindowFunc;
		}

		private void ShowWinWindowFunc()
		{
			uiManager.OnPlayerWin?.Invoke();
			audioManager.StopSound(musicSound);
			audioManager.PlaySound(winSound);
		}
	}
}
