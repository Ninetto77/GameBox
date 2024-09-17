using UnityEngine;

public class EnemyAnimation
{
    private Animator animator;
    private const string WALK_STRING = "Velocity";
    private const string SCREAM_STRING = "Scream";
    private const string DAMAGE_STRING = "Damage";
    private const string ATTACK_STRING = "IsAttacking";
    private const string DEATH_STRING = "Death";


	public EnemyAnimation(Animator animator)
    {
        this.animator = animator;
    }

    public void Walk(float speed)
    {
        animator.SetFloat(WALK_STRING, speed);
    }

	public void Scream()
	{
		animator.SetTrigger(SCREAM_STRING);
	}

	public void Damage()
	{
		animator.SetTrigger(DAMAGE_STRING);
	}

	public void Death()
    {
        animator.SetTrigger(DEATH_STRING);
    }

	public void Attack(bool value)
	{
		animator.SetBool(ATTACK_STRING, value);
	}
}
