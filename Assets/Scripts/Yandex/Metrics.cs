using UnityEngine;
using System.Runtime.InteropServices;
using YG;

namespace Yandex
{
	public static class Metrics
    {
#if !UNITY_EDITOR && UNITY_WEBGL
		[DllImport("__Internal")]
		private static extern void LoadedGame();

		[DllImport("__Internal")]
		private static extern void Level1Win();
		
		[DllImport("__Internal")]
		private static extern void Level1Loose();
#endif


		public static void OnLoadedGame()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			YG2.MetricaSend("LoadedGame");
			//LoadedGame();
#else
			Debug.Log("Metrika.LoadedGame");
#endif
		}
		public static void OnLevel1Win()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			//Level1Win();
			YG2.MetricaSend("Level1Win");
#else
			Debug.Log("Metrika.Level1Complete");
			#endif
		}

		public static void OnLevel1Loose()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			//Level1Loose();
			YG2.MetricaSend("Level1Loose");
#else
			Debug.Log("Metrika.OnLevel1Loose");
#endif
		}

		//О смене задания
		public static void OnChangeTask(int indexTask)
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			
			YG2.MetricaSend("TaskChange", "level_1", indexTask.ToString());
#else
			Debug.Log("Metrika.TaskChange " + indexTask);
#endif
		}
	}
}
