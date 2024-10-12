using System.Collections;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public float Damage;
    public float TimeToStartAttack;
    public ParticleSystem FireParticle;
    public ParticleSystem SparkParticle;

    private OverlapWithAttack overlap;
	private ItemPickup item;
	private bool toolIsPicked;

	private void Start()
	{
		overlap = GetComponent<OverlapWithAttack>();

		ChangeIsPicked();
		item.OnChangeIsPicked += ChangeIsPicked;
	}

	private void ChangeIsPicked()
	{
		item = gameObject.GetComponent<ItemPickup>();
		toolIsPicked = item.IsPicked;
	}

	private void Update()
	{
		if (!toolIsPicked) return;

		if (Input.GetMouseButton(GlobalStringsVars.FIRE))
		{
			StartCoroutine(StartAttack());
		}
		if (Input.GetMouseButtonUp(GlobalStringsVars.FIRE))
		{
			SparkParticle.Play();
			FireParticle.Stop();
		}
	}

	private IEnumerator StartAttack()
	{
		FireParticle.Play();
		SparkParticle.Stop();
		yield return new WaitForSeconds(TimeToStartAttack);
		overlap.PerformAttack();
	}
}
