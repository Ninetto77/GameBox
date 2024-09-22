using System.Threading.Tasks;
using UnityEngine;

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
			case FXType.wood:
				_assetName = "WFX_Wood";
				break;
			case FXType.metal:
				_assetName = "WFX_Metal";
				break;
			case FXType.blaster:
				_assetName = "WFX_Blaster";
				break;
		}
		return LoadAsset<GameObject>(_assetName, position, rotation, parent);
    }

	public void UnloadFX()
	{
		UnloadAsset();
	}
}
