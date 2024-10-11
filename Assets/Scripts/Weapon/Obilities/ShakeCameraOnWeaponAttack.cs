using UnityEngine;
using Attack.Raycast;
using DG.Tweening;
using Code.Global.Animations;
using Attack.Base;

public class ShakeCameraOnWeaponAttack: MonoBehaviour
{
	public ShakeAnimationPreset AnimSettings;

	private AttackBehaviour weapon;
	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void OnEnable()
	{
		weapon = GetComponent<AttackBehaviour>();
		weapon.OnAttackStarted += ShakeCamera;
	}

	private void OnDisable()
	{
		weapon.OnAttackStarted -= ShakeCamera;
	}

	private void ShakeCamera()
	{
		var cameraTransform = mainCamera.transform;
		if (cameraTransform == null) return;

		//AnimationShortCuts.ShakePositionAnimation(cameraTransform, AnimSettings);
		//AnimationShortCuts.ShakeRotationAnimation(cameraTransform, AnimSettings);

		cameraTransform
			.DOShakePosition(0.15f, 0.1f, 10, 90f, false, true, ShakeRandomnessMode.Harmonic)
			.SetEase(Ease.InOutBounce)
			.SetLink(cameraTransform.gameObject);

		cameraTransform
	.DOShakeRotation(0.15f, 0.1f, 10, 90f, true, ShakeRandomnessMode.Harmonic)
	.SetEase(Ease.InOutBounce)
	.SetLink(cameraTransform.gameObject);
	}
}