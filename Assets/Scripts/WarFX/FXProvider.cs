using System.Threading.Tasks;
using UnityEngine;

//namespace AdressableScripts
//{
	public class FXProvider : LocalAssetLoader
	{
		private string _assetName;
		public Task<GameObject> LoadFX(FXType type, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			switch (type)
			{
				case FXType.none:
					_assetName = "";
					break;
				case FXType.fireball:
					_assetName = "Fireball";
					break;
				case FXType.magicArea:
					_assetName = "MagicArea";
					break;
			}
			return LoadAsset<GameObject>(_assetName, position, rotation, parent);
		}

		public void UnloadFX()
		{
			UnloadAsset();
		}
	}
//}
