using DG.Tweening;
using UnityEngine;

namespace CameraSettings
{
	public class CameraShakeAnimation : MonoBehaviour
	{
		private Camera mainCamera;

		private void Start()
		{
			mainCamera = Camera.main;
		}

		public void ReactOnAttack()
		{
			var cameraTransform = mainCamera.transform;

			cameraTransform
				.DOShakePosition(0.15f, 0.5f, 10, 90f, false, true, ShakeRandomnessMode.Harmonic)
				.SetEase(Ease.InOutBounce)
				.SetLink(cameraTransform.gameObject);

			cameraTransform
		.DOShakeRotation(0.15f, 0.5f, 10, 90f, true, ShakeRandomnessMode.Harmonic)
		.SetEase(Ease.InOutBounce)
		.SetLink(cameraTransform.gameObject);
		}
	}
}
