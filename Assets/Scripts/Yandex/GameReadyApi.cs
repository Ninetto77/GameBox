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

		public GameReadyApi() 
		{
#if UNITY_WEBGL && !UNITY_EDITOR

			//OnLoadingAPIReady();
#endif
		}

		//// ����� ���� ������ � ��������������
		//public void OnLoadingAPIReady()
		//{
		//	LoadingAPIReady();
		//}

		// ��� ������ ����
		public void OnGameplayAPIStart()
		{
			GameplayAPIStart();
		}

		// ����/�����/��������
		public void OnGameplayAPIStop()
		{
			GameplayAPIStop();
		}
	}
}
