using UnityEngine;

namespace Old
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletRB : MonoBehaviour
    {
		private float damage;
		public void SetDamage(float weaponDamage)
			{
				damage = weaponDamage;
			}

    }
}