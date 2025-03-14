using Cache;
using DG.Tweening;
using Sounds;
using UnityEngine;
using Zenject;
using Tasks;

public class MoveGates : MonoCache
{
	[Header("Ворота")]
	public Transform gate;
	[Header("Настройки анимации")]
	public float offset;
	public float duration;

    private EnemyKillTask killTask;
	private const string waveGatesSound = GlobalStringsVars.WAVE_GATES_SOUND_NAME;

	[Inject]
	private AudioManager audioManager;
	void Start()
    {
		killTask = GetComponent<EnemyKillTask>();
        if (killTask != null)
        {
            killTask.StartEnemyWave += MoveUp;
            killTask.EndEnemyWave += MoveDown;
		}
    }

	/// <summary>
	/// Поднятие вверх ворот
	/// </summary>
	private void MoveUp()
	{
		audioManager.PlaySound(waveGatesSound);
		gate.DOMoveY(gate.position.y + offset, duration).SetEase(Ease.InOutQuint);
	}

	/// <summary>
	/// Опускание вниз ворот
	/// </summary>
	private void MoveDown()
	{
		audioManager.PlaySound(waveGatesSound);
		gate.DOMoveY(gate.position.y - offset, duration).SetEase(Ease.InOutQuint);
	}

	private void OnDestroy()
	{
		if (killTask != null)
		{
			killTask.StartEnemyWave -= MoveUp;
			killTask.EndEnemyWave -= MoveDown;
		}
	}
}
