using Cache;
using System;
using System.Collections;
using UnityEngine;

namespace Objects.Abilities
{
	public class DissapeareObject : MonoCache
	{
		[Header("Обнаружение игрока")]
		public LayerMask PlayerMask;
		[SerializeField] private float radiusOfDetect = 10;
		[Header("Конечная прозрачность")]
		public float target = 0.0f;
		[Header("Время исчезновения")]
		public float second = 2.0f;

		private Renderer render;
		private Material mat;
		private bool canDissapeare;

		private void Start()
		{
			render = GetComponent<Renderer>();
			mat = render.material;
			canDissapeare = true;
		}
		protected override void OnTick()
		{
			var colliders = Physics.OverlapSphere(transform.position, radiusOfDetect, PlayerMask.value);

			foreach (var collider in colliders)
			{
				if (!canDissapeare) return;

				canDissapeare = false;

				StartCoroutine(FadeImage((getImageDone) =>
				{
					if (getImageDone)
					{
						//code after Fade visible 
						Destroy(this.gameObject);
					}
				}));
			}
		}

		/// <summary>
		/// сделать прозрачным текст
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		private IEnumerator FadeImage(Action<bool> action)
		{
			var alpha = mat.color.a;
			var curcolor = mat.color;
			for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / second)
			{
				var newColor = new Color(curcolor.r, curcolor.g, curcolor.b, Mathf.Lerp(alpha, target, t));
				mat.color = newColor;
				yield return null;
				action(mat.color.a < 0.05f);
			}
		}
	}
}

