using Attack.Base;
using Attack.Raycast;
using Code.Global.Animations;
using UnityEngine;
using Zenject;

public class ShakeWeaponOnAttack : MonoBehaviour
{
	[Inject] private PlayerMoovement player;
	public ShakeAnimationPreset AnimSettings;
	private Transform hand;
	private AttackBehaviour weapon;
	private Camera mainCamera;

	[Inject]
	private void Construct(PlayerMoovement player)
	{
		this.player = player;
	}

	private void OnEnable()
	{
		if (player != null)
			hand = player.hand;
		weapon = GetComponent<AttackBehaviour>();
		weapon.OnAttackStarted += ShakeCamera;
	}

	private void OnDisable()
	{
		weapon.OnAttackStarted -= ShakeCamera;
	}

	private void ShakeCamera()
	{
		var weaponTransform = hand;

		if (weaponTransform == null) return;

		AnimationShortCuts.ShakePositionAnimation(weaponTransform, AnimSettings);
		AnimationShortCuts.ShakeRotationAnimation(weaponTransform, AnimSettings);
	}
}
