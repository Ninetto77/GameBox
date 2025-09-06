using UnityEngine;
using Zenject;

namespace ObjectPoolZenject
{
	public abstract class MyMemoryPool<T> : MemoryPool<T> where T : Component
	{
		protected override void OnCreated(T item)
		{
			// Логика, выполняемая при создании нового объекта в пуле
			// Например, установка начальных значений
			//base.OnCreated(item);
			OnCreateItem(item);
			item.gameObject.SetActive(false);
		}

		protected override void OnSpawned(T item)
		{
			// Логика, выполняемая при взятии объекта из пула
			// Например, активация объекта, сброс значений
			OnBeforeSpawned(item);
			item.gameObject.SetActive(true);
		}

		protected override void OnDespawned(T item)
		{
			// Логика, выполняемая при возвращении объекта в пул
			// Например, деактивация объекта, сброс значений
			item.gameObject.SetActive(false);
			OnAfterDespawned(item);
		}

		protected override void OnDestroyed(T item)
		{
			// Логика, выполняемая при уничтожении объекта (редко используется)
			base.OnDestroyed(item);
		}

		//Виртуальные методы для переопределения в дочерних классах
		protected virtual void OnCreateItem(T item) { }
		protected virtual void OnBeforeSpawned(T item) { }
		protected virtual void OnAfterDespawned(T item) { }
	}
}
