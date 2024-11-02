using Enemy;
using Enemy.States;
using UnityEngine;

[RequireComponent(typeof(OverlapWithAttack))]
public class SmallEnemy : EnemyController, IAttack
{
	private OverlapWithAttack attack;

	private void Start()
	{
		attack = GetComponent<OverlapWithAttack>();
		attack.SetSearchMask(PlayerMask);
	}
	#region �����
	public void Attack()
	{
		if (!canMove) return;

		PlayAttackSound();
		attack.PerformAttack();
	}
	private void PlayAttackSound()
	{
		try
		{
			audioSource.PlayOneShot(attackSound);
		}
		catch (System.Exception)
		{
			Debug.Log($"no sound {attackSound.name}");
		}
	}
	#endregion
}
