using TMPro;
using UnityEngine;

public class InternationalText : MonoBehaviour
{
    [SerializeField] string _en;
    [SerializeField] string _ru;

	private Language lang;

	private void Start()
	{
		if (lang.CurrentLanguage == "en")
			GetComponent<TextMeshProUGUI>().text = _en;
		else
		if (lang.CurrentLanguage == "ru")
			GetComponent<TextMeshProUGUI>().text = _ru;
		else
			GetComponent<TextMeshProUGUI>().text = _en;
	}
}
