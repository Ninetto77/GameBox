using Cache;
using System.Collections;
using UnityEngine;
using Zenject;
using Tasks;

namespace Points
{
	public class CandyPath : MonoCache
	{
		public GameObject[] candyTransforms;

		[Inject] private TaskManager taskManager;
		private bool isFirst = true;
		private void SetVisibleAllCandy()
		{
			for (int i = 0; i < candyTransforms.Length; i++)
			{
				candyTransforms[i].SetActive(false);
			}
		}

		private IEnumerator AppearCandyPath()
		{
			for (int i = 0; i < candyTransforms.Length; i++)
			{
				yield return new WaitForSeconds(1f);
				candyTransforms[i].SetActive(true);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!isFirst) return;

			taskManager.OnEndedTask?.Invoke();
			SetVisibleAllCandy();
			StartCoroutine(AppearCandyPath());
			isFirst = !isFirst;
		}

	}
}
