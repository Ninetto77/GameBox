using Languages;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LocalizedText : MonoBehaviour
{
	[SerializeField]
	private string key;

	[Inject] private Language localizationManager;
	private Text text;

	void Awake()
	{
		if (text == null)
		{
			text = GetComponent<Text>();
		}
		localizationManager.OnLanguageChanged += UpdateText;
	}

	void Start()
	{
		UpdateText();
	}

	private void OnDestroy()
	{
		localizationManager.OnLanguageChanged -= UpdateText;
	}

	virtual protected void UpdateText()
	{
		if (gameObject == null) return;

		if (text == null)
		{
			text = GetComponent<Text>();
		}
		if (text != null) 
			text.text = localizationManager.GetLocalizedValue(key);
	}
}