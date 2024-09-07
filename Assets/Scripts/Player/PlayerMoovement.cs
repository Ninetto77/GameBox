using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class PlayerMoovement : MonoBehaviour
{
    [Header("‘изика")]
    public float Speed = 20f;
    public float Gravity = -9.8f;

	[Header("”дар")]
	[SerializeField] private Transform aimTarget;
	[SerializeField] private Transform hand;

    [Header("—лайдер здоровь€")]
    [SerializeField] private HealthBar healthBar;

    [HideInInspector]
    public Vector3 direction;

    private CharacterController player;
    private Animator animator;
    private PlayerAnimations animations;
    private PlayerBrain brain;
    private PlayerHealth playerHealth;
    private Health health;

	private void Awake()
	{
        health = GetComponent<Health>();
		playerHealth = new PlayerHealth(healthBar, health);
	}
	void Start()
    {
        player = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animations =  new PlayerAnimations(animator);
        brain = new PlayerBrain();

	}

    /// <summary>
    /// ƒвижение игрока по двум направлени€м с учетом гравитации
    /// </summary>
    /// <param name="horizontalDirection">горизонтальное направление</param>
    /// <param name="verticalDirection">вертикальное направление</param>
    public void MovePlayer(float horizontalDirection, float verticalDirection)
    {
        direction = new Vector3(horizontalDirection * Speed, 0, verticalDirection * Speed);
        //ограничение скорости
        direction = Vector3.ClampMagnitude(direction, Speed);

        //гравитаци€
        direction.y = -Gravity;

        direction *= Time.deltaTime;
        direction = transform.TransformDirection(direction);
        //движение
        player.Move(direction);
        animations.SetRuningAnim(player.velocity);

        TurnHead();
        brain.GatherResource();
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
    /// —обытие в анимации, когда надо собрать ресурсы
    /// </summary>
	public void HitAndGeatherResource()
	{
        if (hand == null) return;
        
        if (hand.childCount > 0 && hand.GetChild(0) != null)
        {
            var intrument = hand.GetChild(0);
            intrument.GetComponent<GatherResources>().TryGatherResource();
        }
	}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            health.TakeDamage(10);
        }
    }

		//private void Update()
		//{
		//    if (EventSystem.current.IsPointerOverGameObject())
		//        return;
		//}
	}
