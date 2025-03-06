using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using System.ComponentModel;
using System;

//namespace AdressableScripts
//{
	public class LocalAssetLoader
	{
		private GameObject cachedObject;
		protected async Task<T> LoadAsset<T>(string id, Transform parent = null)
		{
			var handl = Addressables.InstantiateAsync(id, parent);
			cachedObject = await handl.Task;
			if (cachedObject.TryGetComponent(out T component) == false)
			{
				throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
													 "attempt to load it from addressables");
			}
			return component;
		}

		protected async Task<T> LoadAsset<T>(string id, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			var handl = Addressables.InstantiateAsync(id, position, rotation, parent);
			cachedObject = await handl.Task;
			if (cachedObject.TryGetComponent(out T component) == false)
			{
				throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
													 "attempt to load it from addressables");
			}
			return component;
		}

		protected void UnloadAsset()
		{
			if (cachedObject == null) return;
			cachedObject.SetActive(false);
			Addressables.ReleaseInstance(cachedObject);
			cachedObject = null;
		}
	}
//}
