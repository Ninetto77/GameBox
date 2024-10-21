using UnityEngine;
using DG.Tweening;
using Attack.Overlap;

public class PokeWithWeapon : MonoBehaviour
{
	private MelleWeapon weapon;
	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void OnEnable()
	{
		weapon = GetComponent<MelleWeapon>();
		weapon.OnAttackStarted += PokeWith;
	}

	private void OnDisable()
	{
		if (weapon != null)
			weapon.OnAttackStarted -= PokeWith;
	}

	private void PokeWith()
	{
		DOTween.Sequence()
			.Append(
				transform
				.DOLocalMoveZ(0.7f, 0.1f, false)
				.SetEase(Ease.InOutBounce)
				.SetLink(transform.gameObject))
			.AppendInterval(0.1f)
			.Append(transform
				.DOLocalMoveZ(-0.3f, 0.5f, false)
				.SetEase(Ease.InOutBounce)
				.SetLink(transform.gameObject)
				)
		.SetLink(transform.gameObject);
	}
}
