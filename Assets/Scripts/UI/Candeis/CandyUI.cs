using Points;
using TMPro;
using UnityEngine;
using Zenject;

public class CandyUI : MonoBehaviour
{
	[Inject] private ShopPoint shop;
	public TextMeshProUGUI CandyText;
	

	void Start()
    {
        shop.OnChangedPoints += UpdateUI;
	}

	private void UpdateUI(int values)
	{
		CandyText.text = values.ToString();
	}

}
