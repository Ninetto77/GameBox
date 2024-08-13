using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof( ExtractedResource))]
public class HideFields : Editor
{
	private ExtractedResource resource;
	private void OnEnable()
	{
		resource = (ExtractedResource)target;
	}
	public override void OnInspectorGUI()
	{
		//resource.PartOfResource = EditorGUILayout.ObjectField(resource.PartOfResource, typeof(ItemInfo));

		//resource.HasRemainderPrefab = EditorGUILayout.Toggle(resource.HasRemainderPrefab);
		//if (resource.HasRemainderPrefab) 
		//{
		//	//resource.DisolveMaterial = EditorGUILayout.MaskField(resource.DisolveMaterial);
		//	EditorGUILayout.Space();
		//}


		base.OnInspectorGUI();
	}
}
