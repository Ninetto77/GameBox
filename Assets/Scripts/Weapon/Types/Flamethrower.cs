using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public float Damage;
    public float TimeToStartAttack;
    public ParticleSystem particle;

    private OverlapWithAttack overlap;

	private void Start()
	{
		overlap = GetComponent<OverlapWithAttack>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(GlobalStringsVars.FIRE))
		{

		}
	}

	private IEnumerator StartAttack()
	{
		particle.Play();
		yield return new WaitForSeconds(TimeToStartAttack);
		overlap.PerformAttack();
	}
}
