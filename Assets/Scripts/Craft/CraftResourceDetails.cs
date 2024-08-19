using System;
using UnityEngine;
using UnityEngine.UI;

public class CraftResourceDetails : MonoBehaviour
{
	public Text amountText, itemTypeText, totalText, haveText;

	public void FillResourceDetails(string amount, string type, string total, string have)
	{
		amountText.text = amount;
		itemTypeText.text = type;
		totalText.text = total;
		haveText.text = have;
	}
}