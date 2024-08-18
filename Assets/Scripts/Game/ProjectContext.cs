using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    public FXProvider FXProvider { get; private set; }
    public static ProjectContext Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(this);
	}

	public void Initialized()
	{
		FXProvider = new FXProvider ();
	}
}
