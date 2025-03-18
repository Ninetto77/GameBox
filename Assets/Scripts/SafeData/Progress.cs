using System.Runtime.InteropServices;
using UnityEngine;

namespace SaveSystem
{
	public class Progress
	{
		public PlayerInfo playerInfo;

		[DllImport("__Internal")]
		private static extern void SaveExtern(string date);
		[DllImport("__Internal")]
		private static extern void LoadExtern();
		//[DllImport("__Internal")]
		//private static extern void SetLeaderboardScores(string nameLB, int score);

		private const string leaderboardName = "PointsLeaderbourd";

		public Progress()
        {
			playerInfo = new PlayerInfo();
#if UNITY_WEBGL
			//ЯИ
			//LoadExtern();

#endif
		}

		/// <summary>
		/// изменение общего количества очков
		/// </summary>
		public void ChangeCommonPoints()
		{
			playerInfo.PointsCommon = playerInfo.PointsLevel1 + playerInfo.PointsLevel2 + playerInfo.PointsLevel3;
		}


		/// <summary>
		/// сохранение данных
		/// </summary>
		public void SavePlayerInfo()
		{
#if UNITY_WEBGL
			string jsonstring = JsonUtility.ToJson(playerInfo);
			SaveExtern(jsonstring);
			//SetLeaderboardScores(leaderboardName, playerInfo.PointsCommon);
#endif
		}

		/// <summary>
		/// загрузить данные из облака
		/// </summary>
		public void LoadPlayerInfo()
		{
#if UNITY_WEBGL
			LoadExtern();
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
