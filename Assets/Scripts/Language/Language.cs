using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;


namespace Languages
{
	public class Language : MonoBehaviour
	{
		[DllImport("__Internal")]
		private static extern string GetLang();


		public string CurrentLanguage;
		[SerializeField] TextMeshProUGUI _languageText;

		private void Awake()
		{
			CurrentLanguage = GetLang();
			_languageText.text = CurrentLanguage;
		}
	}
}
