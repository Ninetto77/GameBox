using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;


public class BulletUI : MonoBehaviour
{
    public Action<int, int> OnChangeBullets;
	public Action<int> OnChangeCommonBullets;
	public TextMeshProUGUI bulletTxt;
	void Awake()
	{
        OnChangeBullets += ShowBulletsText;
		OnChangeCommonBullets += ChangeCommonBulletsText;
		bulletTxt.text ="";
	}

	private void ShowBulletsText(int cur, int common)
    {
		if (cur == -1 && common == -1)
			bulletTxt.text = "";
		else
			bulletTxt.text = cur.ToString() + "/" + common.ToString();
	}

	private void ChangeCommonBulletsText(int common)
	{
		string pattern = @"/.*$";
		string result = Regex.Replace(bulletTxt.text, pattern, $"/{common}");
		if (result == "") 
			{ bulletTxt.text = "0/" + common; }
		else
			bulletTxt.text = result;
	}

	private void OnDisable()
	{
        OnChangeBullets -= ShowBulletsText;
		OnChangeCommonBullets -= ChangeCommonBulletsText;
	}
}
