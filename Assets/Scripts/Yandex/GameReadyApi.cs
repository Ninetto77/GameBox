using System.Runtime.InteropServices;

namespace Yandex
{
	public class GameReadyApi
    {
#if UNITY_WEBGL

		[DllImport("__Internal")]
		private static extern void LoadingAPIReady();
		[DllImport("__Internal")]
		private static extern void GameplayAPIStart();
		[DllImport("__Internal")]
		private static extern void GameplayAPIStop();
#endif

		public GameReadyApi() { }

		// когда игра готова к взаимодействию
		public void OnLoadingAPIReady()
		{
			LoadingAPIReady();
		}

		// при начале игры
		public void OnGameplayAPIStart()
		{
			GameplayAPIStart();
		}

		// меню/пауза/загрузка
		public void OnGameplayAPIStop()
		{
			GameplayAPIStop();
		}
	}
}
