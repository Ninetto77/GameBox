using Cache;
using Enemy.States;
using System.Collections;
using UnityEngine;
using Zenject;

public class FinalFight : MonoCache
{
	[Header("Трансформы для перемещения")]
    [SerializeField] private Transform witchTransform;
    [SerializeField] private Transform playerTransform;
	[SerializeField] Renderer enemyRenderer;

	private EnemyController witch;
	[Inject] private PlayerMoovement player;

	public void SpawnMagicArea()
	{
		if (witchTransform != null)
		{
			witch = GetComponentInChildren<EnemyController>();
			if (witch != null)
			{
				witch.AnimationEnemy.MagicArea();
				witch.SetDissapeareState(true);
			}
		}
	}

	/// <summary>
	/// Переместить на финально поле боя
	/// </summary>
	public void SpawnOnFinalFight()
    {
        if (witchTransform != null)
        {
			witch = GetComponentInChildren<EnemyController>();
			if (witch != null)
			{
				witch.gameObject.transform.position = witchTransform.position;
				StartCoroutine(ApearBoss());
			}

		}
		if (playerTransform != null)
		{
			player.gameObject.transform.position = playerTransform.position;
		}
	}

	private IEnumerator ApearBoss()
	{
		yield return new WaitForSeconds(0.5f);
		witch.SetDissapeareState(false);
	}
}