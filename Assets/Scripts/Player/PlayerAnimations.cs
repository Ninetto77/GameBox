using UnityEngine;

public class PlayerAnimations
{
    private readonly string animRuningString = "IsRuning";
    private readonly string animRunString = "Run";
    private readonly string animJumpString = "Jump";

    private Animator animator;
    public PlayerAnimations(Animator _animator)
    {
        animator = _animator;
	}

    public void SetRuningAnim(bool state)
    {
		animator.SetBool(animRuningString, state);
	}

	public void SetRuningAnim(Vector3 velocity)
	{
		animator.SetFloat(animRunString, velocity.magnitude);
	}

	public void SetJumpAnim(bool state)
	{
		animator.SetTrigger(animJumpString);
	}
}
