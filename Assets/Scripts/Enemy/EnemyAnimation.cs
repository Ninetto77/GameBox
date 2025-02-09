using UnityEngine;

public class EnemyAnimation
{
    private Animator animator;
    private const string WALK_STRING = "Velocity";
    private const string SCREAM_STRING = "Scream";
    private const string DAMAGE_STRING = "Damage";
    private const string ATTACK_STRING = "IsAttacking";
    private const string LIVE_STRING = "IsLiving";
    private const string DEATH_STRING = "Death";


	public EnemyAnimation(Animator animator)
    {
        this.animator = animator;
        animator.SetBool(LIVE_STRING, true);
    }

    public void Walk(float speed)
    {
        if (animator == null) return;
		animator.SetFloat(WALK_STRING, speed);
    }

	public void Scream()
	{
        if (animator == null) return;
		animator.SetTrigger(SCREAM_STRING);
	}

	public void Damage()
	{
        if (animator == null) return;
		animator.SetTrigger(DAMAGE_STRING);
	}

	public void Death()
    {
        if (animator == null) return;
        animator.SetTrigger(DEATH_STRING);
		animator.SetBool(LIVE_STRING, false);
	}

	public void Attack(bool value)
	{
        if (animator == null) return;
		animator.SetBool(ATTACK_STRING, value);
	}
}
