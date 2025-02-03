using Enemy.States;
using Sounds;
using UnityEngine;
using Zenject;
using SaveSystem;
using Points;
using UnityEngine.SceneManagement;

namespace Enemy.Abilities
{
	public class ShowWinWindow : MonoBehaviour
	{
		[Inject] private UIManager uiManager;
		[Inject] private AudioManager audioManager;
		[Inject] private PointsLevel shop;
		[Inject] private Progress progress;
		[Inject] private PlayerMoovement player;

		private const string winSound = GlobalStringsVars.WIN_SOUND_NAME;
		private const string musicSound = GlobalStringsVars.MAIN_MUSIC_NAME;
		private const string runSound = GlobalStringsVars.RUN_SOUND_NAME;
		private const string walkSound = GlobalStringsVars.WALK_SOUND_NAME;

		private EnemyController enemyController;

		private void Start()
		{
			enemyController = GetComponent<EnemyController>();

			if (enemyController != null )
			{
				enemyController.OnEnemyDeath += ShowWinWindowFunc;
				enemyController.OnEnemyDeath += StopAllActions;
				enemyController.OnEnemyDeath += SaveData;
			}
		}


		/// <summary>
		/// Сохранить данные по поводу уровня
		/// </summary>
		private void SaveData()
		{
			int currentIndexLevel = SceneManager.GetActiveScene().buildIndex;

			////открывается слудующий уровень в зависимости от 
			//// текущего уровня. Например, 
			////после прохождения 1 уровня с индексом 2
			////открывается второй уровень

			////0		 |1	        |2	    |3	    |4	    |
			////mainmenu |loadscene |level1 |level2 |level3 |
			progress.playerInfo.Level = currentIndexLevel;
			switch (currentIndexLevel)
			{
				case 2:
					progress.playerInfo.PointsLevel1 = shop.curPoints.Value;
					break;
				case 3:
					progress.playerInfo.PointsLevel2 = shop.curPoints.Value;
					break;
				case 4:
					progress.playerInfo.PointsLevel3 = shop.curPoints.Value;
					break;
				default:
					break;
			}
			progress.ChangeCommonPoints();
			
			//ЯИ
#if UNITY_WEBGL
			progress.SavePlayerInfo();
#endif
		}

		/// <summary>
		/// Показать окно выигрыша
		/// </summary>
		private void ShowWinWindowFunc()
		{
			uiManager.OnPlayerWin?.Invoke();
		}

		/// <summary>
		/// Остановить врагов и выключить звуки
		/// </summary>
		private void StopAllActions()
		{
			player.OnPlayerWin?.Invoke();
			audioManager.StopSound(musicSound);
			audioManager.StopSound(walkSound);
			audioManager.StopSound(runSound);
			audioManager.PlaySound(winSound);
		}
	}
}
