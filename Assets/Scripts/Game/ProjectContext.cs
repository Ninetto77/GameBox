public class ProjectContext
{ 
    public FXProvider FXProvider { get; private set; }
    //public static ProjectContext Instance { get; private set; }

    public ProjectContext()
    {
		Initialized();
	}

	public void Initialized()
	{
		FXProvider = new FXProvider ();
	}
}
