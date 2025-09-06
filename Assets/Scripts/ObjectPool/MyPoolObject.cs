using UnityEngine;

namespace ObjectPoolZenject
{
	public abstract class MyPoolObject : MonoBehaviour
	{
		public virtual void OnCreate() { }
		public virtual void OnSpawned() { }
		public virtual void OnDespawned() { }
	}
}
