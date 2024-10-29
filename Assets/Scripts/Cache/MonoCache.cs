using System.Collections.Generic;
using UnityEngine;

namespace Cache
{
	public class MonoCache : MonoBehaviour
	{
		public static List<MonoCache> allUpdate = new List<MonoCache>(10001);

		public virtual void OnEnable() => allUpdate.Add(this);

		public virtual void OnDisable() => allUpdate.Remove(this);

		public virtual void OnDestroy() => allUpdate.Remove(this);

		public void Tick() => OnTick();

		protected virtual void OnTick() { }
	}
}