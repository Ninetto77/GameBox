using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClickSound : MonoBehaviour
{
	private readonly string clickSound = GlobalStringsVars.CLICK_SOUND_NAME;
	private Button clickButton;
	[Inject] private AudioManager audioManager;

	private void Start()
	{
		clickButton = GetComponent<Button>();
		clickButton.onClick.AddListener(OnClick);
	}
	private void OnClick()
	{
		audioManager.PlaySound(clickSound);
	}
}
