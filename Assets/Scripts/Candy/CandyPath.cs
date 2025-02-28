using Cache;
using System.Collections;
using UnityEngine;
using Zenject;
using Tasks;

namespace Points
{
	public class CandyPath : MonoCache
	{
		[Header("����� ������")]
		public int numberOfTask = -1;
		[Header("��������� ����")]
		public GameObject[] candyTransforms;

		[Inject] private TaskManager taskManager;
		[Inject] private PointsLevel shop;

		private bool isFirst = true;
		private void Start()
		{
			shop.OnChangedCountCandies += CheckForDelete;
			SetInvisibleAllCandy();
			isFirst = true;
		}

		/// <summary>
		/// ������� ������ ��� ������ ������� ��� �������
		/// </summary>
		private void CheckForDelete()
		{
			if (transform.childCount == 1)
			{
				//Destroy(this);
			}
		}

		/// <summary>
		/// ������� ���������� ��� �������
		/// </summary>
		private void SetInvisibleAllCandy()
		{
			for (int i = 0; i < candyTransforms.Length; i++)
			{
				candyTransforms[i].SetActive(false);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag != "Player") return;

			if (!isFirst) return;

			taskManager.OnEndedTask?.Invoke(numberOfTask);
			StartCoroutine(AppearCandyPath());
			isFirst = !isFirst;
		}

		/// <summary>
		/// ��������� ��������� ������
		/// </summary>
		/// <returns></returns>
		private IEnumerator AppearCandyPath()
		{
			for (int i = 0; i < candyTransforms.Length; i++)
			{
				yield return new WaitForSeconds(1f);
				candyTransforms[i].SetActive(true);
			}
		}

		public override void OnEnable()
		{
			base.OnEnable();
			shop.OnChangedCountCandies -= CheckForDelete;
		}
	}
}
