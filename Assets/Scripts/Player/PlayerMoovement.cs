using UnityEngine;

public class PlayerMoovement : MonoBehaviour
{
    [Header("������")]
    public float Speed = 20f;
    public float Gravity = -9.8f;
    public LayerMask mask;

	[Header("����")]
	[SerializeField] private Transform aimTarget;
	public Transform hand;

    [Header("������� ��������")]
    [SerializeField] private HealthBar healthBar;

    [HideInInspector]
    public Vector3 direction;

    private CharacterController player;
    private Animator animator;
    private PlayerAnimations animations;
    private PlayerBrain brain;
    //private PlayerHealth playerHealth;
    [HideInInspector]
	public Health health;

	private void Awake()
	{
        health = GetComponent<Health>();
		//playerHealth = new PlayerHealth(healthBar, health);
	}
	void Start()
    {
        player = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animations =  new PlayerAnimations(animator);
        brain = new PlayerBrain();

	}

    /// <summary>
    /// �������� ������ �� ���� ������������ � ������ ����������
    /// </summary>
    /// <param name="horizontalDirection">�������������� �����������</param>
    /// <param name="verticalDirection">������������ �����������</param>
    public void MovePlayer(float horizontalDirection, float verticalDirection)
    {
        direction = new Vector3(horizontalDirection * Speed, 0, verticalDirection * Speed);
        //����������� ��������
        direction = Vector3.ClampMagnitude(direction, Speed);

        //����������
        direction.y = -Gravity;

        direction *= Time.deltaTime;
        direction = transform.TransformDirection(direction);
        //��������
        player.Move(direction);
        animations.SetRuningAnim(player.velocity);

        TurnHead();

        brain.Update();
		//animations.SetAimingAnim(true);


		//brain.Shoot(mask);
	}

    private void TurnHead()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        Vector3 desirePosition = ray.origin + ray.direction * 0.7f;
        aimTarget.position = desirePosition;
	}

	public void Hit(bool state)
	{
		animations.SetHitAnim(state);
	}

    /// <summary>
    /// ������� � ��������, ����� ���� ������� �������
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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            health.TakeDamage(10);
        }
    }

    private void GetWeapon()
    {

    }

		//private void Update()
		//{
		//    if (EventSystem.current.IsPointerOverGameObject())
		//        return;
		//}
	}
