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
	#region Атака
	public void Attack()
	{
		attack.PerformAttack();
	}
	#endregion
}
