using System.Runtime.InteropServices;
using UnityEngine;

namespace SaveSystem
{
	public class Progress : MonoBehaviour
	{
		public static Progress instance;
		public PlayerInfo playerInfo;

		[DllImport("__Internal")]
		private static extern void SaveExtern(string date);
		[DllImport("__Internal")]
		private static extern void LoadExtern();

		private void Awake()
		{
			if (instance == null)
			{
				transform.parent = null;
				DontDestroyOnLoad(gameObject);
				instance = this;

#if UNITY_WEBGL
				LoadExtern();
#endif

			}
			else
			{
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// сохранение данных
		/// </summary>
		public void SavePlayerInfo()
		{
#if UNITY_WEBGL
			string jsonstring = JsonUtility.ToJson(playerInfo);
			SaveExtern(jsonstring);
#endif
		}

		/// <summary>
		/// загрузить данные
		/// </summary>
		public void SetPlayerInfo(string value)
		{
#if UNITY_WEBGL
			playerInfo = JsonUtility.FromJson<PlayerInfo>(value);
#endif
		}

	}
}
