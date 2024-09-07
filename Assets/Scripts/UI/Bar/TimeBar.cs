using CommonTimer;

public class TimeBar : Bar
{
	private Timer timer;

	public void Initialize(Timer timer)
	{
		this.timer = timer;
	}

	private void OnEnable()
	{
		if (timer != null) timer.HasBeenUpdate += OnValueChanged;
	}

	private void OnDisable()
	{
		if (timer != null) timer.HasBeenUpdate -= OnValueChanged;
	}
}
