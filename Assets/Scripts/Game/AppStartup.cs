using UnityEngine;

public class AppStartup : MonoBehaviour
{
	private FXProvider fxProvider => ProjectContext.Instance.FXProvider;

	void Start()
    {
		ProjectContext.Instance.Initialized();
		//var fxprovider = await fxProvider.LoadAsset();

	}

}
