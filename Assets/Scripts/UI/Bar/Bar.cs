using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private Slider filter;

	private void Reset()
	{
		if (filter == null) filter = GetComponent<Slider>();
	}

	protected void OnValueChanged(float valueInParts) =>
        filter.value = valueInParts;
}
