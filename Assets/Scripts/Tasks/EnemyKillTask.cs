using Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillTask : TriggerSpawn
{
    public int count;
	public List<EnemyMarker> test;

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
			if (other.tag != "Player") return;

	}
}
