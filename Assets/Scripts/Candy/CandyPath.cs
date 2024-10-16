using Cache;
using System.Collections;
using UnityEngine;

public class CandyPath : MonoCache
{
    public GameObject[] candyTransforms;
	public override void OnEnable()
    {
		base.OnEnable();
		SetVisibleAllCandy();
		StartCoroutine(AppearCandyPath());
    }

	private void SetVisibleAllCandy()
	{
		for (int i = 0; i < candyTransforms.Length; i++)
		{
			candyTransforms[i].SetActive(false);
		}
	}

	private IEnumerator AppearCandyPath()
	{
		for (int i=0; i<candyTransforms.Length; i++)
        {
			yield return new WaitForSeconds(1f);
			candyTransforms[i].SetActive(true);
		}
	}


}
