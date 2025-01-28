using TMPro;
using UnityEngine;

namespace Languages
{
	public class InternationalText : MonoBehaviour
	{
		[SerializeField] string _en;
		[SerializeField] string _ru;

		private Language lang;

		private void Start()
		{
		//	if (lang.currentLanguage == "en")
		//		GetComponent<TextMeshProUGUI>().text = _en;
		//	else
		//	if (lang.currentLanguage == "ru")
		//		GetComponent<TextMeshProUGUI>().text = _ru;
		//	else
		//		GetComponent<TextMeshProUGUI>().text = _en;
		}
	}
}
