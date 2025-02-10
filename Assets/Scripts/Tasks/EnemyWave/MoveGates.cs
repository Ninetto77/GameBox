using Cache;
using DG.Tweening;

public class MoveGates : MonoCache
{
	public float offset;
	public float duration;
    private EnemyKillTask killTask;
	void Start()
    {
		killTask = GetComponent<EnemyKillTask>();
        if (killTask != null)
        {
            killTask.StartEnemyWave += MoveUp;
            killTask.EndEnemyWave += MoveDown;
		}
    }

	private void MoveUp()
	{
		transform.DOMoveY(offset, duration, true).SetEase(Ease.InElastic);
	}

	private void MoveDown()
	{
		transform.DOMoveY(-offset, duration, true).SetEase(Ease.InElastic);
	}
}
