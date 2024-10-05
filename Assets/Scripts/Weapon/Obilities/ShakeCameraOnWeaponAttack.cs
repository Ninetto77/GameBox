using UnityEngine;
using Attack.Raycast;
using DG.Tweening;

public class ShakeCameraOnWeaponAttack: MonoBehaviour
{
	private Weapon weapon;
	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void OnEnable()
	{
		weapon = GetComponent<Weapon>();
		weapon.OnAttackStarted += ShakeCamera;
	}

	private void OnDisable()
	{
		weapon.OnAttackStarted -= ShakeCamera;
	}

	private void ShakeCamera()
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