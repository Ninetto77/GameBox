using UnityEngine;
using Zenject;

public class SpawnEnemy : MonoBehaviour
{
	[SerializeField] private Transform[] Anchours;
	[SerializeField] private GameObject[] Enemies;

	[Inject] private DiContainer container;

	private void Start()
	{
		SpawnEnemies();
	}
	private void SpawnEnemies()
	{
		for (int i = 0; i < Anchours.Length; i++)
		{
			container.InstantiatePrefab(Enemies[i], Anchours[i].position, Quaternion.identity, null);
		}


	}
}
