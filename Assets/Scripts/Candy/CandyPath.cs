using Cache;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Points
{
	[RequireComponent(typeof(Collider))]
	public class CandyPath : MonoCache
	{
		[Header("��������� ����")]
		public GameObject[] candyTransforms;

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
