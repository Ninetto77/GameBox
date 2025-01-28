using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace Languages
{
	public class Language
	{
		[DllImport("__Internal")]
		private static extern string GetLang();

		public delegate void ChangeLangText();
		public event ChangeLangText OnLanguageChanged;

		public static bool isReady = false;
		private string currentLanguage;
		private Dictionary<string, string> localizedText;

		public Language() {
			Debug.Log("Language in construct");
			currentLanguage = GetLang();

			LoadLocalizedText(currentLanguage);
		}

		/// <summary>
		/// доставляет файл с переводом
		/// </summary>
		/// <param name="langName"></param>
		public void LoadLocalizedText(string langName)
		{
			string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json";

			string dataAsJson;

			//если телефон
			if (Application.platform == RuntimePlatform.Android)
			{
				WWW reader = new WWW(path);
				while (!reader.isDone) { }

				dataAsJson = reader.text;
			}
			else
			{
				dataAsJson = File.ReadAllText(path);
			}

			LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

			localizedText = new Dictionary<string, string>();
			for (int i = 0; i < loadedData.items.Length; i++)
			{
				localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
			}

			isReady = true;
			OnLanguageChanged?.Invoke();
		}

		public string GetLocalizedValue(string key)
		{
			if (localizedText.ContainsKey(key))
			{
				return localizedText[key];
			}
			else
			{
				throw new Exception("Localized text with key \"" + key + "\" not found");
			}
		}

		public string CurrentLanguage
		{
			get
			{
				return currentLanguage;
			}
			set
			{
				LoadLocalizedText(value);
			}
		}
		public bool IsReady
		{
			get
			{
				return isReady;
			}
		}
	}
}
