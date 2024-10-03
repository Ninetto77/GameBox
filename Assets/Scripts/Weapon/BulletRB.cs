using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletRB : MonoBehaviour
    {
		private float damage;
		public void SetDamage(float weaponDamage)
			{
				damage = weaponDamage;
			}

		// Start is called before the first frame update
		void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}