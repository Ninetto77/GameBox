using UnityEngine;

public class PlayerAnimations
{
    private readonly string animRunString = "Run";
    private readonly string animJumpString = "Jump";
    private readonly string animHitString = "IsHiting";
    private readonly string animAimingString = "IsAiming";

    private Animator animator;
    public PlayerAnimations(Animator _animator)
    {
        animator = _animator;
	}

	public void SetRuningAnim(Vector3 velocity)
	{
		animator.SetFloat(animRunString, velocity.magnitude);
	}

	public void SetJumpAnim()
	{
		animator.SetTrigger(animJumpString);
	}

	public void SetHitAnim(bool state)
	{
		animator.SetBool(animHitString, state);
	}

	public void SetAimingAnim(bool state)
	{
		animator.SetBool(animAimingString, state);
	}
}
