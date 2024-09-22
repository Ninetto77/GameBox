using System.Collections;
using UnityEngine;

public class Bullet: MonoBehaviour
{
	public float bulletSpeed = 345;
	public float hitForce = 50f;
	public float destroyAfter = 3.5f;

	private float damage;
	private float currentTime = 0;
	private Vector3 newPos;
	private Vector3 oldPos;
	private bool hasHit = false;

	IEnumerator Start()
	{
		newPos = transform.position;
		oldPos = newPos;

		while (currentTime < destroyAfter && !hasHit)
		{
			Vector3 velocity = transform.forward * bulletSpeed;
			newPos += velocity * Time.deltaTime;
			Vector3 direction = newPos - oldPos;
			float distance = direction.magnitude;
			RaycastHit hit;

			// Check if we hit anything on the way
			if (Physics.Raycast(oldPos, direction, out hit, distance))
			{
				if (hit.rigidbody != null)
				{
					//hit.rigidbody.AddForce(direction * hitForce);

					IEntity npc = hit.transform.GetComponent<IEntity>();
					if (npc != null)
					{
						npc.ApplyDamage(damage);
					}
				}

				//newPos = hit.point; //Adjust new position
				StartCoroutine(DestroyBullet());
			}

			currentTime += Time.deltaTime;
			yield return new WaitForFixedUpdate();

			transform.position = newPos;
			oldPos = newPos;
		}

		if (!hasHit)
		{
			StartCoroutine(DestroyBullet());
		}
	}

	public void SetDamage(float weaponDamage)
	{
		damage = weaponDamage;
	}

	IEnumerator DestroyBullet()
	{
		hasHit = true;
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}