using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoovement : MonoBehaviour, IDamageable
{
    [Header("Физика")]
    public float Speed = 40f;
    public float MaxSpeed = 20f;
    public float Gravity = -9.8f;
    public float JumpForce = 5f;

	[Header("Удар")]
	[SerializeField] private Transform aimTarget;
	public Transform hand;

    [HideInInspector]
    public Vector3 direction;

    private CharacterController player;
    private Animator animator;
    private PlayerAnimations animations;
    private PlayerBrain brain;
	private Rigidbody rb;
	//private PlayerHealth playerHealth;
	[HideInInspector]
	public Health health;

    private bool isGrounded;
	private void Awake()
	{
        health = GetComponent<Health>();
	}
	void Start()
    {
        player = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animations =  new PlayerAnimations(animator);
        brain = new PlayerBrain();
        rb = GetComponent<Rigidbody>();
		isGrounded = true;
	}
    private void Update()
    {
		//тест
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            health.TakeDamage(10);
        }
		TurnHead();
		brain.Update();
	}

	/// <summary>
	/// Движение игрока по двум направлениям с учетом гравитации
	/// </summary>
	/// <param name="horizontalDirection">горизонтальное направление</param>
	/// <param name="verticalDirection">вертикальное направление</param>
	public void MovePlayer(float horizontalDirection, float verticalDirection, bool jump)
    {
		direction = new Vector3(horizontalDirection, 0, verticalDirection);

		rb.AddRelativeForce(direction * Speed , ForceMode.Impulse);

		if (rb.velocity.magnitude > MaxSpeed)
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);

		if (isGrounded)
		{
			if (jump == true)
			{
				rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
			}
		}
		animations.SetRuningAnim(rb.velocity);

		//animations.SetAimingAnim(true);
	}

    private void TurnHead()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        Vector3 desirePosition = ray.origin + ray.direction * 0.7f;
        aimTarget.position = desirePosition;
	}

	public void ApplyDamage(float damage) => health.TakeDamage(damage);

	public void Hit(bool state)
	{
		animations.SetHitAnim(state);
	}

    /// <summary>
    /// Событие в анимации, когда надо собрать ресурсы
    /// </summary>
	public void HitAndGeatherResource()
	{
		if (hand == null) return;

		if (hand.childCount > 0 && hand.GetChild(0) != null)
		{
			var intrument = hand.GetChild(0);
			var gather = intrument.GetComponent<GatherResources>();
			if (gather != null)
				gather.TryGatherResource();
		}
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
		if (collision.gameObject.tag == ("Ground"))
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
