using Cache;
using Enemy.States;
using Sounds;
using UnityEngine;
using Zenject;

public class FinalFight : MonoCache
{
	[Header("Трансформы для перемещения")]
    [SerializeField] private Transform witchTransform;
    [SerializeField] private Transform playerTransform;

	private EnemyController witch;
	[Inject] private PlayerMoovement player;

	public void SpawnOnFinalFight()
    {
        if (witchTransform != null)
        {
			witch = GetComponentInChildren<EnemyController>();
			if (witch != null) 
				witch.gameObject.transform.position = witchTransform.position;

		}
		if (playerTransform != null)
		{
			player.gameObject.transform.position = playerTransform.position;
		}
	}
}