using Enemy;
using Enemy.States;

public class SmallEnemy : EnemyController, IAttack
{
	#region Атака
	public void Attack()
	{
		player.ApplyDamage(Damage);
	}
	#endregion
}
