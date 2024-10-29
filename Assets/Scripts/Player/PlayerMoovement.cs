using Cache;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(InputKeys))]
[RequireComponent (typeof(MouseLook))]
public class PlayerMoovement : MonoCache, IDamageable
{
    [Header("Физика")]
    public float Gravity = -9.8f;
    public float JumpForce = 5f;
    [Header("Ходьба")]
    public float Speed = 40f;
    public float MaxSpeed = 20f;

	[Header("Бег")]
	public float RunSpeed = 20f;
	public float RunMaxSpeed = 10f;

	[Header("Удар")]
	[SerializeField] private Transform aimTarget;
	public Transform hand;

	[Header("Подбор предметов")]
	public float reachDistance = 4f;

	[HideInInspector]
    public Vector3 direction;

    private CharacterController player;
    private Animator animator;
    private PlayerAnimations animations;
    private PlayerBrain brain;
	private Rigidbody rb;
	[Inject]
	private UIManager uiManager;
	
	[HideInInspector]
	public Health health;

    private bool isGrounded;
	private float curSpeed;
	private float curMaxSpeed;
	private void Awake()
	{
        health = GetComponent<Health>();
	}
	void Start()
    {
        player = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animations =  new PlayerAnimations(animator);
        brain = new PlayerBrain(reachDistance);
        rb = GetComponent<Rigidbody>();
		isGrounded = true;
	}
	protected override void OnTick()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			ApplyDamage(99f);
		}
		TurnHead();
		brain.Update();
	}

	/// <summary>
	/// Движение игрока по двум направлениям с учетом гравитации
	/// </summary>
	/// <param name="horizontalDirection">горизонтальное направление</param>
	/// <param name="verticalDirection">вертикальное направление</param>
	public void MovePlayer(float horizontalDirection, float verticalDirection, bool jump, bool isRuning)
    {
		direction = new Vector3(horizontalDirection, 0, verticalDirection);

		
		if (isRuning)
		{
			curSpeed = RunSpeed;
			curMaxSpeed = RunMaxSpeed;
		}
		else
		{
			curSpeed = Speed;
			curMaxSpeed = MaxSpeed;
		}

		rb.AddRelativeForce(direction * curSpeed, ForceMode.Impulse);

		if (rb.velocity.magnitude > curMaxSpeed)
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, curMaxSpeed);

		if (isGrounded)
		{
			if (jump == true)
			{
				rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
			}
		}
		animations.SetRuningAnim(rb.velocity);
	}

    private void TurnHead()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        Vector3 desirePosition = ray.origin + ray.direction * 0.7f;
        aimTarget.position = desirePosition;
	}

	public void ApplyDamage(float damage) {
		health.TakeDamage(damage);
		
		if (health.GetCurrentHealth() <=0)
		{
			StartCoroutine(DeadPlayer());
		}
		else 
			uiManager.OnPlayerDamage?.Invoke();
	}

	private IEnumerator DeadPlayer()
	{
		yield return new WaitForEndOfFrame();
		GetComponent<InputKeys>().enabled = false;
		GetComponent<MouseLook>().enabled = false;
		uiManager.OnPlayerDead?.Invoke();
	}

	public void Hit(bool state)
	{
		animations.SetHitAnim(state);
	}
	public void HitAndGeatherResource()
	{

	}
	#region коллизии для прыжка

	void OnCollisionEnter(Collision collision)
	{
		IsGroundedUpate(collision, true);
	}

	void OnCollisionExit(Collision collision)
	{
		IsGroundedUpate(collision, false);
	}

	private void IsGroundedUpate(Collision collision, bool value)
	{
		if (collision.gameObject.tag == "Ground")
		{
			isGrounded = value;
			animations.SetJumpAnim(!value);
		}
	}
	#endregion


	//private void Update()
	//{
	//    if (EventSystem.current.IsPointerOverGameObject())
	//        return;
	//}
}
