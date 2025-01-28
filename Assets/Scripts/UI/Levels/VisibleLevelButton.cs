using Cache;
using UnityEngine.UI;

public class VisibleLevelButton : MonoCache
{
    public LoadingSceneButton PlayButton;
    public MenuManager menuManager;

	private Button button;
	private void Start()
	{
		button = PlayButton.GetComponent<Button>();
		button.interactable = false;
		menuManager.OnChangeLevelDescription += SetLevel;
	}

	private void SetLevel(int sceneName)
	{
		switch (sceneName)
		{
			case 0:
				PlayButton.SceneName = "Nick2";
				button.interactable = true;
				break;
			case 1:
				PlayButton.SceneName = "Nick2";
				button.interactable = false;
				break;
			case 2:
				PlayButton.SceneName = "Nick2";
				button.interactable = false;
				break;
			default:
				PlayButton.SceneName = "Nick2";
				button.interactable = false;
				break;
		}
	}

	public void OpenLevelButtons(int openLevels)
	{

	}
}
