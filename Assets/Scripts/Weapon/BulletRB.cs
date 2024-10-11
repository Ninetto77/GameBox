using UnityEngine;

namespace Old
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletRB : MonoBehaviour
    {
		private float damage;
		private float speed;
		private Rigidbody rb;

		private void Start()
		{
			rb = GetComponent<Rigidbody>();
		}
		public void AddForce(float speed)
		{
			this.speed = speed;
		}

		private void FixedUpdate()
		{
			GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
		}

		public void SetDamage(float weaponDamage)
			{
				damage = weaponDamage;
			}

    }
}