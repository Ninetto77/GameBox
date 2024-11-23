using System;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    public Action<int, int> OnChangeBullets;
    public TextMeshProUGUI bulletTxt;
	void Awake()
	{
        OnChangeBullets += ShowBullets;
		bulletTxt.text = 0 + "/" + 1000;

	}

	private void ShowBullets(int cur, int common)
    {
		bulletTxt.text = cur.ToString() + "/" + common.ToString();
	}

	private void OnDisable()
	{
        OnChangeBullets -= ShowBullets;
	}
}
