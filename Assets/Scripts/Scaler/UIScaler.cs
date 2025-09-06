using UnityEngine;

public class UIScaler : MonoBehaviour
{
	[Header("Settings")]
	public float minScale = 0.7f;
	public float maxScale = 1.2f;
	public float referenceWidth = 1920f;

	private RectTransform rectTransform;

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		UpdateScale();
	}

	void Update()
	{
		if (Screen.width != rectTransform.rect.width)
		{
			UpdateScale();
		}
	}

	void UpdateScale()
	{
		float scale = Mathf.Clamp(Screen.width / referenceWidth, minScale, maxScale);
		rectTransform.localScale = new Vector3(scale, scale, 1);
	}
}